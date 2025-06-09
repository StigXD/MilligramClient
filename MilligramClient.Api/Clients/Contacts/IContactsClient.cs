using MilligramClient.Domain.Dtos;

namespace MilligramClient.Api.Clients.Contacts;

public interface IContactsClient
{
    Task<ChatDto[]> GetContactsAsync(CancellationToken cancellationToken = default);
    Task<ChatDto[]> CreateContactAsync(CancellationToken cancellationToken = default);
    Task<ChatDto[]> DeleteContactsAsync(CancellationToken cancellationToken = default);
}