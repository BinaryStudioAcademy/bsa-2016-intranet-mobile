using System;
using Windows.UI.Xaml.Data;

namespace Intranet.WindowsUWP.Views.Converters
{
    public class BoolToStarOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolBalue = (bool) value;
            return boolBalue ? 1 : 0.2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}