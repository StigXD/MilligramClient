namespace MilligramClient.Domain.Dtos;

public class ContactDto
{
	public string Name { get; set; }
	public string? AddedUserNickname { get; set; }
	public Guid AddedUserId { get; set; }
}