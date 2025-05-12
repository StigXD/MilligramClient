using MilligramClient.Wpf.Enums;

namespace MilligramClient.Wpf.Entities;

public class Cell
{
	public CellValues Value { get; set; }

	public Cell()
	{
		Value = CellValues.None;
	}
}