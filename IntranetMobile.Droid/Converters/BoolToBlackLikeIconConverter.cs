using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    internal class BoolToBlackLikeIconConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "ic_favorite_black_24dp" : "ic_favorite_border_black_24dp";
        }
    }
}