using System.ComponentModel;
using System.Windows;
using MilligramClient.Wpf.Dispatcher;
using MilligramClient.Wpf.Models;
using MilligramClient.Wpf.Services.View;

namespace MilligramClient.Wpf.Views.MainWindow.Logic;

public class MainWindowProvider : IMainWindowProvider
{
	private readonly IViewService _viewService;
	private readonly IDispatcherHelper _dispatcherHelper;
	private readonly MainWindowViewModel.IFactory _gameViewModelFactory;
	
    private Window _gameWindow;

	public MainWindowProvider(
		IViewService viewService,
		IDispatcherHelper dispatcherHelper,
		MainWindowViewModel.IFactory gameViewModelFactory)
	{
		_viewService = viewService;
		_dispatcherHelper = dispatcherHelper;
		_gameViewModelFactory = gameViewModelFactory;
	}

	public void Show(UserModel user)
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() =>
		{
			_gameWindow ??= CreateWindow(user);
			_gameWindow.Show();
		});
    }
	public void CloseIfCreated()
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() => _gameWindow?.Close());
	}

	private Window CreateWindow(UserModel user)
	{
		var viewModel = _gameViewModelFactory.Create(user);
		var window = _viewService.CreateWindow(viewModel, WindowMode.Other);
		
		window.Closing += OnWindowClosing;
		return window;
	}

    private void OnWindowClosing(object sender, CancelEventArgs e)
	{
		_gameWindow.Closing -= OnWindowClosing;
		_gameWindow = null;
	}
}