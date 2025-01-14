using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Final
{
    public class Renk : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string yon = value as string;
            return yon?.StartsWith("-") ?? false ? Colors.Red : Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
