using System;
using System.Globalization;
using Android.Text;
using MvvmCross.Platform.Converters;

namespace IntranetMobile.Droid.Views.Util
{
    public class FromHtmlValueConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return Html.FromHtml(value, new ImageGetter(), null);
        }
    }
}