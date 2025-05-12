using System.ComponentModel;
using System.Windows;
using MilligramClient.Wpf.Dispatcher;
using MilligramClient.Wpf.Services.View;

namespace MilligramClient.Wpf.Views.AuthenticationWindow.Logic;

public class AuthWindowProvider : IAuthWindowProvider
{
	private readonly IViewService _viewService;
	private readonly IDispatcherHelper _dispatcherHelper;
	private readonly AuthViewModel.IFactory _authViewModelFactory;


	private Window _authWindow;

	public AuthWindowProvider(IViewService viewService, IDispatcherHelper dispatcherHelper, AuthViewModel.IFactory authViewModelFactory)
	{
		_viewService = viewService;
		_dispatcherHelper = dispatcherHelper;
		_authViewModelFactory = authViewModelFactory;
	}

	public void Show()
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() =>
		{
			_authWindow ??= CreateWindow();
			Application.Current.MainWindow = _authWindow;
			_authWindow.Show();
		});
	}

	public void CloseIfCreated()
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() => _authWindow?.Close());
	}

	private Window CreateWindow()
	{
		var window = _viewService.CreateWindow<AuthViewModel>(WindowMode.Main);
		window.Closing += OnWindowClosing;
		return window;
	}

	private void OnWindowClosing(object sender, CancelEventArgs e)
	{
		_authWindow.Closing -= OnWindowClosing;
		_authWindow = null;
	}
}