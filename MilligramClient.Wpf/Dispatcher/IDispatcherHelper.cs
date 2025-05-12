﻿using System.Windows.Threading;

namespace MilligramClient.Wpf.Dispatcher;

public interface IDispatcherHelper
{
	System.Windows.Threading.Dispatcher UiDispatcher { get; }

	void CheckBeginInvokeOnUI(Action action);
	DispatcherOperation RunAsync(Action action);
	void Reset();
}