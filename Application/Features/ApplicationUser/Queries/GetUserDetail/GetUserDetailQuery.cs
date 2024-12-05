using Domain.Dtos.ApplicationUser;
using MediatR;

namespace Application.Features.ApplicationUser.Queries.GetUserDetail;

public record GetUserDetailQuery(Guid id) : IRequest<UserVm>;
