using GalaSoft.MvvmLight;

namespace MilligramClient.Wpf.Models;

public class UserModel : ViewModelBase, ICloneable
{
	private string _username;
	private int _highScore;
	private bool _isRememberUser;
	public UserModel()
	{
		Username = string.Empty;
		HighScore = 0;
		IsRememberUser = false;
	}

    public string Username
	{
		get => _username;
		set => Set(ref _username, value);
	}

	public int HighScore
	{
		get => _highScore;
		set => Set(ref _highScore, value);
	}

	public bool IsRememberUser
	{
		get => _isRememberUser;
		set => Set(ref _isRememberUser, value);
	}

	public object Clone()
	{
		var clone = (UserModel) MemberwiseClone();
		return clone;
	}
}