using Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Repositories;

public class SignalRService : ISignalRService
{
    //private readonly IHubContext<ChatHub> _hubContext;

    //public SignalRService(IHubContext<ChatHub> hubContext)
    //{
    //    _hubContext = hubContext;
    //}

    public Task AddToGroup(string connectionId, string chatId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMessage(string chatId, string MessageId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromGroup(string connectionId, string chatId)
    {
        throw new NotImplementedException();
    }

    public Task SendMessage(string chatId, string userId, string message)
    {
        throw new NotImplementedException();
    }
}
