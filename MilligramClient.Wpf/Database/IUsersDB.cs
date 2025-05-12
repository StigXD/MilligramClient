using MilligramClient.Wpf.Models;

namespace MilligramClient.Wpf.Database;

public interface IUsersDB
{
	List<UserModel> Users { get; }

	void Add(UserModel user);
	void Read();
	void Write();
}