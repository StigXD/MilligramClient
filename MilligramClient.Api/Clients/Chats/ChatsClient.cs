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

	public Task<ChatDto> GetChatAsync(Guid id,
									  CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto>(
				Method.Get,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<ChatDto> CreateChatAsync(ChatDto newChat,
										 CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto>(
				Method.Post,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<ChatDto> UpdateChatAsync(Guid id,
										 ChatDto updatedChat,
										 CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto>(
				Method.Put,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task DeleteChatAsync(Guid id,
								CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync(
				Method.Delete,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<UserDto[]> GetUsersAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<UserDto[]>(
				Method.Get,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<ChatDto> AddUserAsync(Guid idChat,
									  Guid idUser,
									  CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto>(
				Method.Post,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<ChatDto> DeleteUserAsync(Guid idChat,
										 Guid idUser,
										 CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<ChatDto>(
				Method.Delete,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<MessageDto[]> GetMessagesAsync(
		CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<MessageDto[]>(
				Method.Get,
				"api/chats",
				token,
				cancellationToken));
	}
	public Task<MessageDto> GetMessageAsync(Guid chatId,
											Guid messageId,
											CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<MessageDto>(
				Method.Get,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<MessageDto> AddMessageAsync(Guid chatId,
											MessageDto messageDto,
											CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<MessageDto>(
				Method.Post,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task<MessageDto> UpdateMessageAsync(Guid chatId,
											   Guid messageId,
											   MessageDto messageDto,
											   CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync<MessageDto>(
				Method.Put,
				"api/chats",
				token,
				cancellationToken));
	}

	public Task DeleteMessageAsync(Guid chatId,
								   Guid messageId,
								   CancellationToken cancellationToken = default)
	{
		return _tokenProvider.ExecuteWithToken(token =>
			SendRequestAsync(
				Method.Delete,
				"api/chats",
				token,
				cancellationToken));
	}
}