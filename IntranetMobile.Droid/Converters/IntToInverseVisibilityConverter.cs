using System;
using Android.Views;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Converters
{
    public class IntToInverseVisibilityConverter : MvxValueConverter<int, ViewStates>
    {
        protected override ViewStates Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == 0 ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}