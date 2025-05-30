﻿using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight;
using MilligramClient.Common.Wpf.Dispatcher;
using MilligramClient.Common.Wpf.View;

namespace MilligramClient.Wpf.Views.Main.Logic;

public class MainWindowProvider : IMainWindowProvider
{
	private readonly IViewService _viewService;
	private readonly IDispatcherHelper _dispatcherHelper;

	private Window? _mainWindow;

	public MainWindowProvider(IViewService viewService, IDispatcherHelper dispatcherHelper)
	{
		_viewService = viewService;
		_dispatcherHelper = dispatcherHelper;
	}

	public void Show()
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() =>
		{
			_mainWindow ??= CreateWindow();
			Application.Current.MainWindow = _mainWindow;
			_mainWindow.Show();
		});
	}

	public void CloseIfCreated()
	{
		_dispatcherHelper.CheckBeginInvokeOnUI(() => _mainWindow?.Close());
	}

	private Window CreateWindow()
	{
		var window = _viewService.CreateWindow<MainViewModel>(WindowMode.Window);
		window.Closing += OnWindowClosing;
		return window;
	}

	private void OnWindowClosing(object? sender, CancelEventArgs e)
	{
		if (_mainWindow == null)
			return;

		_mainWindow.Closing -= OnWindowClosing;
		(_mainWindow.DataContext as ICleanup)?.Cleanup();
		_mainWindow = null;
	}
}