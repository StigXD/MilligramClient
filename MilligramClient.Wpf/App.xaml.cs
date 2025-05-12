using System.Windows;
using MilligramClient.Wpf.Database;
using MilligramClient.Wpf.Views.AuthenticationWindow.Logic;


namespace MilligramClient.Wpf;

/// <summary>
/// Логика взаимодействия для App.xaml
/// </summary>
public partial class App
{
	private readonly IUsersDB _usersDB;
	private readonly IAuthWindowProvider _authWindowProvider;

	public App(IUsersDB usersDB, IAuthWindowProvider authWindowProvider)
	{
		_usersDB = usersDB;
		_authWindowProvider = authWindowProvider;

		InitializeComponent();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		_usersDB.Read();
		_authWindowProvider.Show();
	}

	protected override void OnExit(ExitEventArgs e)
	{
		_usersDB.Write();
		base.OnExit(e);
	}
}