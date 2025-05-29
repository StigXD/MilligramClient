using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.IconPacks;
using MilligramClient.Api.Token;
using MilligramClient.Common.Wpf.MessageBox;
using MilligramClient.Services.Token;
using MilligramClient.Wpf.Views.Login.Logic;

namespace MilligramClient.Wpf.Views.Main;

public class DemoModel : MainViewModel
{
	public DemoModel() : base(
		DemoLocator.Locate<IMessenger>(),
		DemoLocator.Locate<ITokenStorage>(),
		DemoLocator.Locate<ITokenProvider>(),
		DemoLocator.Locate<IMessageBoxService>(),
		DemoLocator.Locate<ILoginWindowProvider>())
	{
    }
}