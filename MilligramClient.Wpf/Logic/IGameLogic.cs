namespace MilligramClient.Wpf.Logic
{
    public interface IGameLogic
	{
		public void NewField();
		public void GetNewValue();
		public void PlayStep();
        public void Restart();
        public void Exit();
	}
}
