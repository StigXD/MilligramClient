using MilligramClient.Wpf.Models;

namespace MilligramClient.Wpf.Views.MainWindow.Logic;

public interface IMainWindowProvider
{
	void Show(UserModel user);
	void CloseIfCreated();
}