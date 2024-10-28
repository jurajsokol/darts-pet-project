using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace Darts.MVVM.Converters
{
    public class BoolToAccentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color c = (Color)Application.Current.Resources["SystemAccentColor"];
            if (value is bool boolValue && boolValue)
            { 
                return new SolidColorBrush() { Color = c };
            }
            return new SolidColorBrush() { Color = Colors.Transparent };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
