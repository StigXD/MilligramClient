using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MilligramClient.Api.Exceptions;
using MilligramClient.Api.Token;
using MilligramClient.Common;
using MilligramClient.Common.Extensions;
using MilligramClient.Common.Wpf.Base;
using MilligramClient.Common.Wpf.Commands;
using MilligramClient.Common.Wpf.MessageBox;
using MilligramClient.Domain.Dtos;
using MilligramClient.Wpf.Messages;

namespace MilligramClient.Wpf.Views.Login.Controls.Login;

public class LoginControlViewModel : ViewModel<LoginControl>, IDataErrorInfo
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();

    private readonly IMessenger _messenger;
    private readonly ITokenProvider _tokenProvider;
    private readonly IMessageBoxService _messageBoxService;

    private readonly ExecutionTracker _executionTracker;

    private bool _isBusy;
    private string _email;
    private string _password;

    private ICommand _cleanEmailCommand;
    private ICommand _loginCommand;
    private ICommand _registerCommand;

    public override object Header => string.Empty;

    public bool IsBusy
    {
        get => _isBusy;
        set => Set(ref _isBusy, value);
    }

    public string Email
    {
        get => _email;
        set => Set(ref _email, value);
    }

    public string Password
    {
        get => _password;
        set => Set(ref _password, value);
    }

    public ICommand CleanEmailCommand => _cleanEmailCommand ??= new RelayCommand(OnCleanEmail);
    public ICommand LoginCommand => _loginCommand ??= new AsyncRelayCommand(OnLoginAsync, CanLogin);
    public ICommand RegisterCommand => _registerCommand ??= new RelayCommand(OnRegister);

    public LoginControlViewModel(
        IMessenger messenger,
        ITokenProvider tokenProvider,
        IMessageBoxService messageBoxService)
    {
        _messenger = messenger;
        _tokenProvider = tokenProvider;
        _messageBoxService = messageBoxService;

        _executionTracker = new ExecutionTracker(() => IsBusy = true, () => IsBusy = false);

        Refresh();
    }

    public void Refresh()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    private void OnCleanEmail()
    {
        Email = string.Empty;
    }

    private async Task OnLoginAsync()
    {
        using (_executionTracker.TrackExecution())
        {
            await LoginAsync().ConfigureAwait(false);

            _messenger.Send(new CloseLoginWindowMessage());
        }
    }

    private async Task LoginAsync()
    {
        try
        {
            var loginDto = new LoginDto { Login = Email, Password = Password };
            await _tokenProvider.LoginAsync(loginDto).ConfigureAwait(false);
        }
        catch (SendRequestException exception) when (exception.StatusCode == HttpStatusCode.Unauthorized)
        {
            _messageBoxService.Show("Некорректные логин и(или) пароль", "Ошибка");
        }
    }

    private bool CanLogin()
    {
        return Email.IsSignificant() && Password.IsSignificant() && !HasError;
    }

    private void OnRegister()
    {
        ChangeLoginWindow(LoginState.Register);
    }

    private void ChangeLoginWindow(LoginState loginState)
    {
        _messenger.Send(new ChangeLoginWindowMessage(loginState));
    }

    public string Error => string.Empty;

    private ObservableCollection<string> _errors;

    public virtual ObservableCollection<string> Errors
    {
        get
        {
            _errors = new ObservableCollection<string>();
            var error = this[nameof(Email)];
            if (!string.IsNullOrEmpty(error))
                _errors.Add(error);
            error = this[nameof(Password)];
            if (!string.IsNullOrEmpty(error))
                _errors.Add(error);
            return _errors;
        }
    }

    public virtual bool HasError => Errors?.Count > 0;

    public string this[string columnName]
    {
        get
        {
            var error = string.Empty;
            switch (columnName)
            {
                case nameof(Email):
                {
                    if (string.IsNullOrEmpty(Email))
                        break;

                    if (!EmailAddressAttribute.IsValid(Email))
                        error = "Некорректный Email";

                    break;
                }

                case nameof(Password):
                {
                    if (string.IsNullOrEmpty(Password))
                        break;

                    if (Password.Length < 5)
                        error = "Пароль должен состоять минимум из 5 символов";

                    break;
                }
            }

            return error;
        }
    }
}