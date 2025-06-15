using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MilligramClient.Api.Clients.Chats;
using MilligramClient.Api.Clients.Contacts;
using MilligramClient.Api.Clients.SendMessage;
using MilligramClient.Api.Token;
using MilligramClient.Common.Wpf.Base;
using MilligramClient.Common.Wpf.Dispatcher;
using MilligramClient.Common.Wpf.MessageBox;
using MilligramClient.Common.Wpf.Messages;
using MilligramClient.Domain.Model;
using MilligramClient.Domain.Dtos;
using MilligramClient.Services.Token;
using MilligramClient.Wpf.Views.Login.Logic;

namespace MilligramClient.Wpf.Views.Main;

public class MainViewModel : ViewModel<MainWindow>, INotifyPropertyChanged
{
	private string _login;
	private string _newMessageText;
	private string _statusMessage;
	private HamburgerMenuItem _selectedMenuItem;
	private ChatDto _selectedChat;
	private ContactDto _selectedContact;
	private MainWindowState _mainWindowState = MainWindowState.Chats;

	private ICommand _contentRenderedCommand;
	private ICommand _logoutCommand;
	private ICommand _exitCommand;
	private ICommand _menuCommand;
	private ICommand _sendMessageCommand;
	private ICommand _attachFileCommand;

	public override object Header => "Milligram";

	private readonly IMessenger _messenger;
	private readonly ITokenStorage _tokenStorage;
	private readonly ITokenProvider _tokenProvider;
	private readonly IMessageBoxService _messageBoxService;
	private readonly ILoginWindowProvider _loginWindowProvider;
	private readonly IChatsClient _chatsClient;
	private readonly IContactsClient _contactsClient;
	private readonly ISendMessageClient _sendMessageClient;
	private readonly IDispatcherHelper _dispatcherHelper;

	public HamburgerMenuItems Menu { get; } = new HamburgerMenuItems();
	public ObservableCollection<HamburgerMenuItem> OptionsItems { get; }
	public ObservableCollection<MessageModel> Messages { get; } = new ObservableCollection<MessageModel>();
	public ObservableCollection<ChatDto> Chats { get; set; } = new ObservableCollection<ChatDto>();
	public ObservableCollection<ContactDto> Contacts { get; set; } = new ObservableCollection<ContactDto>();

	public MainWindowState MainWindowState
	{
		get => _mainWindowState;
		set => Set(ref _mainWindowState, value);
	}

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
			if (value != null)
			{
				OnMenuSelected(value.Tag.ToString());
			}
		}
	}

	public ChatDto SelectedChat
	{
		get => _selectedChat;
		set => Set(ref _selectedChat, value);
	}

	public ContactDto SelectedContact
	{
		get => _selectedContact;
		set => Set(ref _selectedContact, value);
	}


	// Команды
	public ICommand SendMessageCommand => _sendMessageCommand ??= new RelayCommand(SendMessage);
	public ICommand AttachFileCommand => _attachFileCommand ??= new RelayCommand(AttachFile);
	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new RelayCommand(OnContentRendered);
	public ICommand MenuCommand => _menuCommand ??= new RelayCommand<string>(OnMenuSelected);
	public ICommand LogoutCommand => _logoutCommand ??= new RelayCommand(OnLogout);
	public ICommand ExitCommand => _exitCommand ??= new RelayCommand(OnExit);
	public ICommand SelectChatCommand => new RelayCommand(OnChatSelected);

	public MainViewModel(
		IMessenger messenger,
		ITokenStorage tokenStorage,
		ITokenProvider tokenProvider,
		IMessageBoxService messageBoxService,
		ILoginWindowProvider loginWindowProvider,
		IChatsClient chatsClient,
		IContactsClient contactsClient,
		ISendMessageClient sendMessageClient,
		IDispatcherHelper dispatcherHelper)
	{
		_messenger = messenger;
		_tokenStorage = tokenStorage;
		_tokenProvider = tokenProvider;
		_messageBoxService = messageBoxService;
		_loginWindowProvider = loginWindowProvider;
		_chatsClient = chatsClient;
		_contactsClient = contactsClient;
		_sendMessageClient = sendMessageClient;
		_dispatcherHelper = dispatcherHelper;

		// Пример сообщений (в реальном приложении будет загрузка из сервера)
		Messages.Add(new MessageModel
		{
			Sender = "Система",
			Text = "Добро пожаловать в чат!",
			Timestamp = DateTime.Now.ToString("HH:mm")
		});
	}

	public void OnMenuSelected(string tag)
	{
		foreach (var item in Menu.MenuItems)
		{
			item.IsVisible = Visibility.Collapsed;
		}

		switch (tag)
		{
			case "contacts":
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "newContact").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "deleteContact").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "back").IsVisible = Visibility.Visible;

				MainWindowState = MainWindowState.Contacts;
				if (!Contacts.Any())
					GetAllContacts();

				break;
			case "chats":
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "newChat").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "newPrivateChat").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "deleteChat").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "back").IsVisible = Visibility.Visible;

				MainWindowState = MainWindowState.Chats;
				if (!Chats.Any())
					GetAllChats();

				break;
			case "settings":
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "back").IsVisible = Visibility.Visible;
				break;
			case "logOut":
				LogoutCommand.Execute(null);
				break;
			case "back":
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "contacts").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "chats").IsVisible = Visibility.Visible;
				Menu.MenuItems.FirstOrDefault(i => i.Tag.ToString() == "settings").IsVisible = Visibility.Visible;
				break;
		}
	}

	private async Task GetAllChats()
	{
		try
		{
			Chats.Clear(); // Очищаем предыдущие чаты
			Console.WriteLine("Запрос чатов...");
			var chats = await _chatsClient.GetChatsAsync().ConfigureAwait(false);
			Console.WriteLine($"Получено чатов: {chats?.Count() ?? 0}");
			// Обновляем коллекцию в UI-потоке
			_dispatcherHelper.CheckBeginInvokeOnUI(() =>
			{
				if (chats != null)
				{
					foreach (var chat in chats)
					{
						Console.WriteLine($"Чат: {chat.Name}, Owner: {chat.OwnerUserId}");
						Chats.Add(chat);
					}

					if (Chats.Any())
					{
						SelectedChat = Chats.First();
						Console.WriteLine($"Выбран чат: {SelectedChat.Name}");
					}
					else
					{
						StatusMessage = "Чаты не найдены";
					}
				}
				else
				{
					StatusMessage = "Сервер вернул пустой список чатов";
				}
			});
		}
		catch (Exception ex)
		{
			StatusMessage = $"Ошибка: {ex.Message}";
			Console.WriteLine($"Ошибка при получении чатов: {ex}");
		}
	}

	private async Task GetAllContacts()
	{
		try
		{
			Contacts.Clear();
			Console.WriteLine("Запрос контактов...");
			var contacts = await _contactsClient.GetContactsAsync().ConfigureAwait(false);
			Console.WriteLine($"Получено контактов: {contacts?.Count() ?? 0}");

			_dispatcherHelper.CheckBeginInvokeOnUI(() =>
			{
				if (contacts != null)
				{
					foreach (var contact in contacts)
					{
						Console.WriteLine($"Контакт: {contact.Name}");
						Contacts.Add(contact);
					}

					if (Contacts.Any())
					{
						SelectedContact = Contacts.First();
						Console.WriteLine($"Выбран контакт: {SelectedContact.Name}");
					}
					else
					{
						StatusMessage = "Контакты не найдены";
					}
				}
				else
				{
					StatusMessage = "Сервер вернул пустой список контактов";
				}
			});
		}
		catch (Exception ex)
		{
			StatusMessage = $"Ошибка: {ex.Message}";
			StatusMessage = $"Ошибка загрузки контактов: {ex.Message}";
		}
	}

	private void OnChatSelected()
	{
		//SelectedChat = chat;
		// Здесь можно добавить загрузку сообщений для выбранного чата
		// Очищаем предыдущие сообщения
		Messages.Clear();

		// Загружаем сообщения для выбранного чата
		//var messages = await _sendMessageClient.GetChatMessagesAsync(chat.Id).ConfigureAwait(false);

		//foreach (var message in messages)
		//{
		//	Messages.Add(new MessageModel
		//	{
		//		Sender = message.,
		//		Text = message.Text,
		//		Timestamp = message.Timestamp.ToString("HH:mm")
		//	});
		//}

		// Автоскролл к последнему сообщению
		if (Messages.Any())
		{
			// Здесь нужно добавить код для скролла к последнему сообщению
			// (обычно это делается через поведение или в code-behind)
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

		//var sendMessage = await _testClient.GetTestStringAsync().ConfigureAwait(false);
		//_messageBoxService.Show(testString, "Ответ от сервера");


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

		Login = _tokenProvider.GetLoginFromToken();
	}

	private void OnLogout()
	{
		_tokenProvider.Logout();
		_tokenStorage.SaveToken(null);
		_loginWindowProvider.Show();
	}

	public override void Cleanup()
	{
		base.Cleanup();
	}
}