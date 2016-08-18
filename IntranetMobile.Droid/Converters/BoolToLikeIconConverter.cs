using System;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid
{
    public class BoolToLikeIconConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ? "ic_favorite_white_24dp" : "ic_favorite_border_white_24dp";
        }
    }
}

