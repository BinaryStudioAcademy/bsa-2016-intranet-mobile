using System;
using Windows.UI.Xaml.Data;
using IntranetMobile.Core;

namespace Intranet.WindowsUWP.Converters
{
    public class BinaryStudioHtmlConvertor : IValueConverter
    {
        private const string DownHtmlLayout = "</body></html>";

        private static string UpperHtmlLayout
            =>
                $@"<html><head>
            <meta name=""viewport"" content=""user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width"">
            <link href=""{Constants
                    .BaseUrl}app/styles/css/style.css"" rel=""stylesheet"">
            <link href=""{Constants.BaseUrl}styles/css/libs.css"" rel=""stylesheet"">
            <link href=""{Constants
                        .BaseUrl}styles/css/style.css"" rel=""stylesheet"">
            <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"">
            <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css"">
            <style>pre{{white-space:pre-wrap;white-space:-moz-pre-wrap;white-space:-pre-wrap;white-space:-o-pre-wrap;word-wrap:break-word;}}</style>
            <style>div{{max-width:100%;width:auto;height:auto}}</style><style>img{{display:inline;height:auto;max-width:100%}}</style>
            <style>body{{font-family:'Roboto';margin:8px}}</style>
            </head><body>"
            ;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return UpperHtmlLayout + value + DownHtmlLayout;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}