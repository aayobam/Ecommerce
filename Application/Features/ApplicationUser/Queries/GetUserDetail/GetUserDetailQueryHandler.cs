﻿using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Dtos.ApplicationUser;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationUser.Queries.GetUserDetail;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserVm>
{
    private readonly ILogger<GetUserDetailQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;

    public GetUserDetailQueryHandler(ILogger<GetUserDetailQueryHandler> logger, IMapper mapper, IUserRepository<Domain.Entities.ApplicationUser> userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserVm> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var instance = await _userRepository.GetByIdAsync(request.Id);
        if (instance == null)
        {
            _logger.LogInformation($"{nameof(instance)} - {request.Id} not found | {DateTimeOffset.Now}");
            throw new EcommerceNotFoundException(nameof(instance), request.Id, HttpStatusCode.NotFound.ToString());
        }
        var data = _mapper.Map<UserVm>(instance);
        return data;
    }
}