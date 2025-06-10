﻿using MilligramClient.Domain.Dtos;

namespace MilligramClient.Api.Clients.Chats;

public interface IChatsClient
{
	Task<ChatDto[]> GetChatsAsync(CancellationToken cancellationToken = default);
	Task<ChatDto> GetChatAsync(Guid id, CancellationToken cancellationToken = default);
	Task<ChatDto> CreateChatAsync(ChatDto newChat, CancellationToken cancellationToken = default);
	Task<ChatDto> UpdateChatAsync(Guid id, ChatDto updatedChat, CancellationToken cancellationToken = default);
	Task DeleteChatAsync(Guid id, CancellationToken cancellationToken = default);
	Task<UserDto[]> GetUsersAsync(CancellationToken cancellationToken = default);
	Task<ChatDto> AddUserAsync(Guid idChat, Guid idUser, CancellationToken cancellationToken = default);
	Task<ChatDto> DeleteUserAsync(Guid idChat, Guid idUser, CancellationToken cancellationToken = default);
	Task<MessageDto[]> GetMessagesAsync(CancellationToken cancellationToken = default);
	Task<MessageDto> GetMessageAsync(Guid chatId, Guid messageId, CancellationToken cancellationToken = default);
	Task<MessageDto> AddMessageAsync(Guid chatId, MessageDto messageDto, CancellationToken cancellationToken = default);
	Task<MessageDto> UpdateMessageAsync(Guid chatId, Guid messageId, MessageDto messageDto, CancellationToken cancellationToken = default);
	Task DeleteMessageAsync(Guid chatId, Guid messageId, CancellationToken cancellationToken = default);
}