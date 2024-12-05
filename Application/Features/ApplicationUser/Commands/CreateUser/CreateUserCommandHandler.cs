using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Dtos.ApplicationUser;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationUser.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserVm>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserRepository <Domain.Entities.ApplicationUser>userRepository, IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator(_userRepository);
        var validationResult = validator.Validate(request);
        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n {validationResult} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }
        var model = _mapper.Map<Domain.Entities.ApplicationUser>(request);
        await _userRepository.CreateAsync(model);
        var data = _mapper.Map<UserVm>(request);
        return data;
    }
}
