namespace MilligramClient.Domain.Dtos;

public class MessageDto
{
	Guid? Id { get; set; }
	public string? Text { get; set; }
	FileDto? FileDto { get; set; }
	public DateTime CreationTime { get; set; }
	public DateTime LastChangeTime { get; set; }
	public Guid UserId { get; set; }
	public string? UserNickname { get; set; }
}