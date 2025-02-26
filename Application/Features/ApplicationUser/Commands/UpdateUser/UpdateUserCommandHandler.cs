using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Dtos.ApplicationUser;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationUser.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserVm>
{
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;

    public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IMapper mapper, IUserRepository<Domain.Entities.ApplicationUser> userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserVm> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var instance = await _userRepository.GetByIdAsync(request.Id);
        if (instance == null)
        {
            _logger.LogInformation($"\n {nameof(instance)} - {request.Id} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException("User not found", HttpStatusCode.NotFound.ToString());
        }
        var validator = new UpdateUserCommandValidator(_userRepository);
        var validationResult = validator.Validate(request);
        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n {validationResult} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }
        var model = _mapper.Map<Domain.Entities.ApplicationUser>(request);
        await _userRepository.UpdateAsync(model);
        var data = _mapper.Map<UserVm>(request);
        return data;
    }
}
