namespace MilligramClient.Domain.Dtos;

public class ContactDto
{
<<<<<<< HEAD
	public string Name { get; set; }
=======
    public Guid Id { get; set; }
    public string Name { get; set; }
>>>>>>> master
	public string? AddedUserNickname { get; set; }
	public Guid AddedUserId { get; set; }
}