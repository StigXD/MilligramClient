using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MilligramClient.Wpf.Base;
using MilligramClient.Wpf.Enums;
using MilligramClient.Wpf.Messages;
using MilligramClient.Wpf.Models;

namespace MilligramClient.Wpf.Views.MainWindow;

public class MainWindowViewModel : ViewModel<MainWindow>
{
	public override object Header => "Milligram";
	private UserModel _user;
	private readonly IMessenger _messenger;

	private Random _rnd;

	public UserModel User
	{
		get => _user;
		set => Set(ref _user, value);
	}

	private ICommand _exitCommand;


	public ICommand ExitCommand => _exitCommand ??= new RelayCommand(OnExit);

	public MainWindowViewModel(IMessenger messenger, UserModel user)
	{
		_messenger = messenger;
		User = user;
		_rnd = new Random();
	}

	private CellValues GetRandomValue() => _rnd.Next(1, 100) >= 90 ? CellValues.Four : CellValues.Two;


	private void OnExit() // вызов методя для выхода из игры
	{
		_messenger.Send(new RequestCloseMessage(this, null));
	}

	public interface IFactory
	{
		MainWindowViewModel Create(UserModel user);
	}
}