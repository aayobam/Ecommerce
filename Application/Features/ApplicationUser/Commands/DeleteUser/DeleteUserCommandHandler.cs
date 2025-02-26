using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationUser.Commands.DeleteUser;

public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, GenericResponse>
{
    private readonly ILogger<RemoveUserRoleCommandHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser>  _userRepository;

    public RemoveUserRoleCommandHandler(ILogger<RemoveUserRoleCommandHandler> logger, IUserRepository<Domain.Entities.ApplicationUser> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<GenericResponse> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var instance = await _userRepository.GetByIdAsync(request.id);

        if (instance == null)
        {
            _logger.LogInformation($"\n user not found | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException("user not found", HttpStatusCode.NotFound.ToString());
        }

        await _userRepository.DeleteAsync(instance);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            StatusCode = HttpStatusCode.OK.ToString()
        };
    }
}
