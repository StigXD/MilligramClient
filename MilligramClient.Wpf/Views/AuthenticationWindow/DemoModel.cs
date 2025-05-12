using GalaSoft.MvvmLight.Messaging;

namespace MilligramClient.Wpf.Views.AuthenticationWindow;

public class DemoModel : AuthViewModel
{
	public DemoModel() : base(Messenger.Default, null, null)
	{
		//CpuList = new ObservableCollection<CpuModel>(
		//	new[]
		//	{
		//		new CpuModel { Name = "Intel Core i7-12700K" },
		//		new CpuModel { Name = "Intel Core i7-13700K" },
		//		new CpuModel { Name = "Intel Core i7-14700K" }
		//	});
	}
}