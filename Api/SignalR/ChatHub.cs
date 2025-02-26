using Microsoft.AspNetCore.SignalR;

namespace Api.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(string username, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", username, message);
    }

    public async Task SendGroupMessage(string chatId, string username, string message)
    {
        var timestamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        await Clients.Group(chatId).SendAsync("ReceiveMessage", username, message, timestamp);
    }

    public async Task JoinChat(string chatId, string userName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task LeaveChat(string chatId, string userName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }
}
