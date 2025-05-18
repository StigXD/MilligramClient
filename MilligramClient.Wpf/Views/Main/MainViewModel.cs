using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MilligramClient.Common.Wpf.Base;
using MilligramClient.Common.Wpf.Messages;
using MilligramClient.Wpf.Models;

namespace MilligramClient.Wpf.Views.Main;

public class MainViewModel : ViewModel<MainWindow>
{
	public override object Header => "Milligram";
	private readonly IMessenger _messenger;

	private ICommand _exitCommand;


	public ICommand ExitCommand => _exitCommand ??= new RelayCommand(OnExit);

	public MainViewModel(IMessenger messenger)
	{
		_messenger = messenger;
	}

	private void OnExit() // вызов методя для выхода из игры
	{
		_messenger.Send(new RequestCloseMessage(this, null));
	}

	public interface IFactory
	{
		MainViewModel Create(UserModel user);
	}
}