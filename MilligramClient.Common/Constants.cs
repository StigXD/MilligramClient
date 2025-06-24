namespace MilligramClient.Common;

public static class Constants
{
	public const string ServerAddress = "http://192.168.3.80:18690";
	public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);

	public const int RefreshSlidingTokenBeforeExpirationInPercent = 50;

	public const string RegistryApplicationName = "MilligramClientAuthorizeExample";
	public const string RegistrySettingsKey = $@"SOFTWARE\{RegistryApplicationName}";
}