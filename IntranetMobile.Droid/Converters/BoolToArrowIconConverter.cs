using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    public class BoolToArrowIconConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "ic_keyboard_arrow_up_black_24dp" : "ic_keyboard_arrow_down_black_24dp";
        }
    }
}