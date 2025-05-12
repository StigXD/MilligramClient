namespace MilligramClient.Wpf.Entities;

public class User
{
	public string Username { get; set; }
	public int HighScore { get; set; }
	public bool IsRememberUser { get; set; }

	public User() : this(string.Empty) { }

	public User(string name)
	{
		Username = name;
		HighScore = 0;
		IsRememberUser = false;
	}
}