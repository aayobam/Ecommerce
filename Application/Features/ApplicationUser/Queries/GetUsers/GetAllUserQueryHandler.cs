using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Dtos.ApplicationUser;
using MediatR;

namespace Application.Features.ApplicationUser.Queries.GetAllUser;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserVm>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;

    public GetAllUserQueryHandler(IMapper mapper, IUserRepository<Domain.Entities.ApplicationUser> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<UserVm>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        var data = _mapper.Map<List<UserVm>>(users);
        return data;
    }
}
