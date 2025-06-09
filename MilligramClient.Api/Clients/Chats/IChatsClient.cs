using MilligramClient.Domain.Dtos;

namespace MilligramClient.Api.Clients.Chats;

public interface IChatsClient
{
    Task<ChatDto[]> GetChatsAsync(CancellationToken cancellationToken = default);
    Task<ChatDto[]> CreatePrivateChatAsync(CancellationToken cancellationToken = default);
    Task<ChatDto[]> CreateGroupChatAsync(CancellationToken cancellationToken = default);
    Task<ChatDto[]> DeleteChatAsync(CancellationToken cancellationToken = default);
}