using System.Windows;
using MilligramClient.Wpf.Views.Login.Logic;
using MilligramClient.Wpf.Views.Main.Logic;


namespace MilligramClient.Wpf;

public partial class App
{
	private readonly IMainWindowProvider _mainWindowProvider;
	private readonly ILoginWindowProvider _loginWindowProvider;

	public App(
		IMainWindowProvider mainWindowProvider,
		ILoginWindowProvider loginWindowProvider)
	{
		_mainWindowProvider = mainWindowProvider;
		_loginWindowProvider = loginWindowProvider;

		InitializeComponent();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		_loginWindowProvider.Show();
	}
}