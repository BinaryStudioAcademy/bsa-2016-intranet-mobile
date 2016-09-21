using System;
using Windows.UI.Xaml.Data;

namespace Intranet.WindowsUWP.Views.Converters
{
    class BoolToBlackLikeIconConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = (bool) value ? "../../Images/ic_favorite_black_24dp.png" : "../../Images/ic_favorite_border_black_24dp.png";

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
