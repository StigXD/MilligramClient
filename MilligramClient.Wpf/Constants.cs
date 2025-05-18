namespace MilligramClient.Wpf;

public static class Constants
{
	public const string ServerAddress = "http://localhost:42312";
	public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);

	public const int RefreshSlidingTokenBeforeExpirationInPercent = 50;
}