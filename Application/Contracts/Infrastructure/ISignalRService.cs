namespace Application.Contracts.Infrastructure;

public interface ISignalRService
{
    Task SendMessage(string chatId, string userId, string message);
    Task AddToGroup(string connectionId, string chatId);
    Task RemoveFromGroup(string connectionId, string chatId);
    Task DeleteMessage(string chatId, string MessageId);
}
