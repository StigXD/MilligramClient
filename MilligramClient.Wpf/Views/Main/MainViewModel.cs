using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.IconPacks;
using MilligramClient.Api.Token;
using MilligramClient.Common.Wpf.Base;
using MilligramClient.Common.Wpf.MessageBox;
using MilligramClient.Common.Wpf.Messages;
using MilligramClient.Services.Token;
using MilligramClient.Wpf.Views.Login.Logic;

namespace MilligramClient.Wpf.Views.Main;

public class MainViewModel : ViewModel<MainWindow>, INotifyPropertyChanged
{
	private string _login;

	private ICommand _contentRenderedCommand;
	private ICommand _logoutCommand;
	private ICommand _testServerCommand;
	private ICommand _exitCommand;

	public override object Header => "Milligram";

	private readonly IMessenger _messenger;
	private readonly ITokenStorage _tokenStorage;
	private readonly ITokenProvider _tokenProvider;
	private readonly IMessageBoxService _messageBoxService;
	private readonly ILoginWindowProvider _loginWindowProvider;

	public ObservableCollection<HamburgerMenuItem> MenuItems { get; set; }
	public ObservableCollection<HamburgerMenuItem> OptionsItems { get; }

    public string Login
	{
		get => _login;
		set => Set(ref _login, value);
	}

	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new RelayCommand(OnContentRendered);

	public ICommand LogoutCommand => _logoutCommand ??= new RelayCommand(OnLogout);

	//public ICommand TestServerCommand => _testServerCommand ??= new AsyncRelayCommand(OnTestServer);
	public ICommand ExitCommand => _exitCommand ??= new RelayCommand(OnExit);

	public MainViewModel(
		IMessenger messenger,
		ITokenStorage tokenStorage,
		ITokenProvider tokenProvider,
		IMessageBoxService messageBoxService,
		ILoginWindowProvider loginWindowProvider)
	{
		_messenger = messenger;
		//_tokenStorage = tokenStorage;
		//_tokenProvider = tokenProvider;
		_messageBoxService = messageBoxService;
		_loginWindowProvider = loginWindowProvider;

		MenuItems = new ObservableCollection<HamburgerMenuItem>
		{
			new HamburgerMenuItem { Label = "Contacts", Icon = new PackIconIonicons { Kind = PackIconIoniconsKind.ContactsMD }, Tag = "contacts" },
			new HamburgerMenuItem { Label = "Chats", Icon = new PackIconEntypo { Kind = PackIconEntypoKind.Chat }, Tag = "chats" },
			new HamburgerMenuItem { Label = "Settings", Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccountCog }, Tag = "settings" }
		};

		OptionsItems = new ObservableCollection<HamburgerMenuItem>
		{
			new HamburgerMenuItem { Label = "Log out", Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Logout }, Tag = "logOut" }

		};
	}

	private void HamburgerMenuControl_OnItemInvoked()
	{

	}
    private void OnExit()
	{
		_messenger.Send(new RequestCloseMessage(this, null));
	}

	private void OnContentRendered()
	{
		_loginWindowProvider.CloseIfCreated();

		//Login = _tokenProvider.GetLoginFromToken();
	}

	private void OnLogout()
	{
		_tokenProvider.Logout();
		_tokenStorage.SaveToken(null);
		_loginWindowProvider.Show();
	}

	//private async Task OnTestServer()
	//{
	//	var testString = await _testClient.GetTestStringAsync().ConfigureAwait(false);
	//	_messageBoxService.Show(testString, "Ответ от сервера");
	//}

	public override void Cleanup()
	{
		base.Cleanup();
	}
}