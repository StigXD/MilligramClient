using System.Globalization;
using MilligramClient.Wpf.Converters.Base;
using MilligramClient.Wpf.Enums;

namespace MilligramClient.Wpf.Converters;
public class CellValuesToIntConverter: MarkupConverterBase
{
	protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is not CellValues v)
			return default;

		return v switch
		{
			CellValues.Two => 2,
			CellValues.Four => 4,
			CellValues.Eight => 8,
			CellValues.Sixteen => 16,
			CellValues.ThirtyTwo => 32,
			CellValues.SixtyFour => 64,
			CellValues.OneHundredTwentyEight => 128,
			CellValues.TwoHundredFiftySix => 256,
			CellValues.FiveHundredTwelve => 512,
			CellValues.OneThousandTwentyFour => 1024,
			CellValues.TwoThousandFortyEight => 2048,
			_ => default(object)
		};
	}

	protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}