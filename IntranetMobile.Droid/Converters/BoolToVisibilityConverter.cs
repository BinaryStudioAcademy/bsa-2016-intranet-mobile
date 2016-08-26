using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    public class BoolToVisibilityConverter : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value)
            {
                return ViewStates.Visible;
            }
            return ViewStates.Gone;
        }
    }
}