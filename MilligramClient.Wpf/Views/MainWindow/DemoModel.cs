using GalaSoft.MvvmLight.Messaging;

namespace MilligramClient.Wpf.Views.MainWindow;

public class DemoModel : MainWindowViewModel
{
	public DemoModel() : base(Messenger.Default, null)
	{
	}
}