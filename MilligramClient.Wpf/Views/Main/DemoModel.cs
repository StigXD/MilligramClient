using GalaSoft.MvvmLight.Messaging;

namespace MilligramClient.Wpf.Views.Main;

public class DemoModel : MainViewModel
{
	public DemoModel() : base(DemoLocator.Locate<IMessenger>())
	{
	}
}