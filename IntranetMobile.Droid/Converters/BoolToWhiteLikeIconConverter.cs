using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    public class BoolToWhiteLikeIconConverter : MvxValueConverter<bool, long>
    {
        protected override long Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? Resource.Drawable.ic_favorite_white_24dp : Resource.Drawable.ic_favorite_border_white_24dp;
        }
    }
}