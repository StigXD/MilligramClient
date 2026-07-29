namespace MilligramClient.Common;

public static class Constants
{
<<<<<<< HEAD
	public const string ServerAddress = "http://192.168.3.97:5193";
=======
	public const string ServerAddress = "http://92.55.7.199:8181";
>>>>>>> master
	public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);

	public const int RefreshSlidingTokenBeforeExpirationInPercent = 50;

	public const string RegistryApplicationName = "MilligramClientAuthorizeExample";
	public const string RegistrySettingsKey = $@"SOFTWARE\{RegistryApplicationName}";
}