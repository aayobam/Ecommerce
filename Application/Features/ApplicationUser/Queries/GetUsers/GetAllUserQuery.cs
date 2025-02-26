using Domain.Dtos.ApplicationUser;
using MediatR;

namespace Application.Features.ApplicationUser.Queries.GetAllUser;

public class GetAllUserQuery : IRequest<List<UserVm>>;
