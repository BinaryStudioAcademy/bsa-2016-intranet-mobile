using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    public class BoolToArrowIconConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ?  "ic_keyboard_arrow_up_black_24dp" : "ic_keyboard_arrow_down_black_24dp";
        }
    }
}