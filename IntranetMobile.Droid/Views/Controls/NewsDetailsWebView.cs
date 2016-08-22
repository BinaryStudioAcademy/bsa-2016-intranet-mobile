using System;
using Android.Content;
using Android.Util;
using Android.Webkit;
using IntranetMobile.Droid.Views.Util;

namespace IntranetMobile.Droid.Views.Controls
{
    public class NewsDetailsWebView : WebView
    {
        private const string DownHtmlLayout = "</body></html>";
        private string _text;

        public NewsDetailsWebView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        private string UpperHtmlLayout
            =>
                $@"<html><head>
            <meta name=""viewport"" content=""user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width"">
            <link href=""http://team.binary-studio.com/app/styles/css/style.css"" rel=""stylesheet"">
            <link href=""http://team.binary-studio.com/styles/css/libs.css"" rel=""stylesheet"">
            <link href=""http://team.binary-studio.com/styles/css/style.css"" rel=""stylesheet"">
            <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"">
            <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css"">
            <style>pre{{white-space:pre-wrap;white-space:-moz-pre-wrap;white-space:-pre-wrap;white-space:-o-pre-wrap;word-wrap:break-word;}}</style>
            <style>div{{max-width:100%;width:auto;height:auto}}</style><style>img{{display:inline;height:auto;max-width:100%}}</style>
            <style>body{{font-family:'Roboto';margin:{DisplayUtils
                    .ConvertDpToPixel(8, Context)}px}}</style>
            </head><body>";

        public string Text
        {
            get { return _text; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _text = value;

                var htmlString = UpperHtmlLayout + _text + DownHtmlLayout;

                LoadData(htmlString, "text/html; charset=utf-8", "utf-8");
                UpdatedHtmlContent();
            }
        }

        public event EventHandler HtmlContentChanged;

        private void UpdatedHtmlContent()
        {
            var handler = HtmlContentChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}