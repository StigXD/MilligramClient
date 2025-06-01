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
using MilligramClient.Domain.Model;
using MilligramClient.Services.Token;
using MilligramClient.Wpf.Views.Login.Logic;

namespace MilligramClient.Wpf.Views.Main;

public class MainViewModel : ViewModel<MainWindow>, INotifyPropertyChanged
{
	private string _login;
	private string _newMessageText;
	private string _statusMessage;
	private HamburgerMenuItem _selectedMenuItem;

	private ICommand _contentRenderedCommand;
	private ICommand _logoutCommand;
	private ICommand _testServerCommand;
	private ICommand _exitCommand;
	private ICommand _menuCommand;

	public override object Header => "Milligram";

	private readonly IMessenger _messenger;
	private readonly ITokenStorage _tokenStorage;
	private readonly ITokenProvider _tokenProvider;
	private readonly IMessageBoxService _messageBoxService;
	private readonly ILoginWindowProvider _loginWindowProvider;

	public ObservableCollection<HamburgerMenuItem> MenuItems { get; set; }
	public ObservableCollection<HamburgerMenuItem> OptionsItems { get; }
	public ObservableCollection<MessageModel> Messages { get; } = new ObservableCollection<MessageModel>();

	public string Login
	{
		get => _login;
		set => Set(ref _login, value);
	}

	public string NewMessageText
	{
		get => _newMessageText;
		set => Set(ref _newMessageText, value);
	}


	public string StatusMessage
	{
		get => _statusMessage;
		set => Set(ref _statusMessage, value);
	}

	public HamburgerMenuItem SelectedMenuItem
	{
		get => _selectedMenuItem;
		set
		{
			Set(ref _selectedMenuItem, value);
			if (value != null) OnMenuSelected(value.Tag.ToString());
		}
	}

	// Команды
	public ICommand SendMessageCommand { get; }
	public ICommand AttachFileCommand { get; }
	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new RelayCommand(OnContentRendered);
	public ICommand MenuCommand => _menuCommand ??= new RelayCommand<string>(OnMenuSelected);

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
			new HamburgerMenuItem { Label = "Contacts", Icon = PackIconIoniconsKind.ContactsMD, Tag = "contacts" },
			new HamburgerMenuItem { Label = "Chats", Icon = PackIconIoniconsKind.ChatboxesMD, Tag = "chats" },
			new HamburgerMenuItem { Label = "Settings", Icon = PackIconIoniconsKind.SettingsMD, Tag = "settings" }
		};

		OptionsItems = new ObservableCollection<HamburgerMenuItem>
		{
			new HamburgerMenuItem { Label = "Log out", Icon = PackIconIoniconsKind.LogOutMD, Tag = "logOut" }
		};
		SendMessageCommand = new RelayCommand(SendMessage, () => !string.IsNullOrWhiteSpace(NewMessageText));
		AttachFileCommand = new RelayCommand(AttachFile);

		// Пример сообщений (в реальном приложении будет загрузка из сервера)
		Messages.Add(new MessageModel
		{
			Sender = "Система",
			Text = "Добро пожаловать в чат!",
			Timestamp = DateTime.Now.ToString("HH:mm")
		});
	}

	private void OnMenuSelected(string tag)
	{
		switch (tag)
		{
			case "HomeView":
				SelectedView = new HomeView();
				break;
			case "ProfileView":
				SelectedView = new ProfileView();
				break;
			case "SettingsView":
				SelectedView = new SettingsView();
				break;
			case "Logout":
				LogoutCommand.Execute(null);
				break;
		}
	}

	private void SendMessage()
	{
		if (string.IsNullOrWhiteSpace(NewMessageText)) return;

		Messages.Add(new MessageModel
		{
			Sender = "Вы",
			Text = NewMessageText,
			Timestamp = DateTime.Now.ToString("HH:mm")
		});

		NewMessageText = string.Empty;
		StatusMessage = "Сообщение отправлено";
	}

	private void AttachFile()
	{
		var openFileDialog = new Microsoft.Win32.OpenFileDialog();
		if (openFileDialog.ShowDialog() == true)
		{
			StatusMessage = $"Прикреплен файл: {openFileDialog.FileName}";
			// Здесь можно добавить логику обработки файла
		}
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