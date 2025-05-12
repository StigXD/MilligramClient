using System.Globalization;
using MilligramClient.Wpf.Converters.Base;
using MilligramClient.Wpf.Enums;

namespace MilligramClient.Wpf.Converters;

public class CellValuesToBackgroundConverter: MarkupConverterBase
{
	protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is not CellValues v)
			return default;

		var x = Utile.stateValueColor.TryGetValue(CellValues.None, out var c);
		return v switch
		{
			CellValues.None => Utile.stateValueColor[CellValues.None],
			CellValues.Two => Utile.stateValueColor[CellValues.Two],
			CellValues.Four => Utile.stateValueColor[CellValues.Four],
			CellValues.Eight => Utile.stateValueColor[CellValues.Eight],
			CellValues.Sixteen => Utile.stateValueColor[CellValues.Sixteen],
			CellValues.ThirtyTwo => Utile.stateValueColor[CellValues.ThirtyTwo],
			CellValues.SixtyFour => Utile.stateValueColor[CellValues.SixtyFour],
			CellValues.OneHundredTwentyEight => Utile.stateValueColor[CellValues.OneHundredTwentyEight],
			CellValues.TwoHundredFiftySix => Utile.stateValueColor[CellValues.TwoHundredFiftySix],
			CellValues.FiveHundredTwelve => Utile.stateValueColor[CellValues.FiveHundredTwelve],
			CellValues.OneThousandTwentyFour => Utile.stateValueColor[CellValues.OneThousandTwentyFour],
			CellValues.TwoThousandFortyEight => Utile.stateValueColor[CellValues.TwoThousandFortyEight],
			_ => default(object)
		};
	}

	protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}