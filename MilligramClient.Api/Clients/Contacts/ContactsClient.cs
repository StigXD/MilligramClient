using MilligramClient.Api.Token;
using MilligramClient.Domain.Dtos;
using RestSharp;

namespace MilligramClient.Api.Clients.Contacts;

public class ContactsClient : HttpClientBase, IContactsClient
{
	private readonly ITokenProvider _tokenProvider;

	public ContactsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
		: base(address, timeout)
	{
		_tokenProvider = tokenProvider;
	}

	public Task<ChatDto[]> GetContactsAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Get,
				"api/contacts",
				token,
				cancellationToken));
	}

	public Task<ChatDto[]> CreateContactAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Post,
				"api/createContacts",
				token,
				cancellationToken));
	}

	public Task<ChatDto[]> DeleteContactsAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Get,
				"api/deleteContacts",
				token,
				cancellationToken));
	}
}