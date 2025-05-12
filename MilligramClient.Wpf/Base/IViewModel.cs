using System.Windows;

namespace MilligramClient.Wpf.Base;

public interface IViewModel
{
	object Header { get; }
	FrameworkElement View { get; }
}