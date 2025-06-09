using MilligramClient.Api.Token;
using MilligramClient.Domain.Dtos;
using RestSharp;

namespace MilligramClient.Api.Clients.Chats;

public class ChatsClient : HttpClientBase, IChatsClient
{
	private readonly ITokenProvider _tokenProvider;

	public ChatsClient(ITokenProvider tokenProvider, string address, TimeSpan timeout)
		: base(address, timeout)
	{
		_tokenProvider = tokenProvider;
	}

	public Task<ChatDto[]> GetChatsAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Get,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<ChatDto[]> CreatePrivateChatAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Post,
				"api/createPrivateChat",
				token,
				cancellationToken));
	}

	public Task<ChatDto[]> CreateGroupChatAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Post,
				"api/createPublicChat",
				token,
				cancellationToken));
	}

	public Task<ChatDto[]> DeleteChatAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto[]>(
				Method.Get,
				"api/deleteChat",
				token,
				cancellationToken));
	}
}