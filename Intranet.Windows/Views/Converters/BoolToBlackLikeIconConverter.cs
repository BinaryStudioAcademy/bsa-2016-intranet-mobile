using System;
using Windows.UI.Xaml.Data;

namespace Intranet.WindowsUWP.Views.Converters
{
    class BoolToBlackLikeIconConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "\uE00B" : "\uE006";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
