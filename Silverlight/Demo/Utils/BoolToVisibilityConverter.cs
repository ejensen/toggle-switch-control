using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Demo.Utils
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType == typeof(Visibility))
			{
				var visible = System.Convert.ToBoolean(value, culture);
				return visible ? Visibility.Visible : Visibility.Collapsed;
			}
			throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
