using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    public class BoolToWhiteLikeIconConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "ic_favorite_white_24dp" : "ic_favorite_border_white_24dp";
        }
    }
}