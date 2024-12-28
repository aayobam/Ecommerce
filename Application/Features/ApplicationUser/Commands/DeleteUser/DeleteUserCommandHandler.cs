using Application.Contracts.Persistence;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationUser.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser>  _userRepository;

    public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserRepository<Domain.Entities.ApplicationUser> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var instance = await _userRepository.GetByIdAsync(request.id);
        if (instance == null)
        {
            _logger.LogInformation($"\n {nameof(instance)} - {request.id} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException(nameof(instance), request.id, HttpStatusCode.NotFound.ToString());
        }
        await _userRepository.DeleteAsync(instance);
        return Unit.Value;
    }
}
