namespace MilligramClient.Wpf.Messages;

public class MainWindowMenuSelectedMessage
{
	public string Tag { get; }

	public MainWindowMenuSelectedMessage(string tag)
	{
		Tag = tag;
	}
}