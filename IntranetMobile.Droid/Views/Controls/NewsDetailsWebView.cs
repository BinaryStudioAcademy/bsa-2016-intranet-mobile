using System;
using Android.Content;
using Android.Util;
using Android.Webkit;

namespace IntranetMobile.Droid.Views.Controls
{
    public class NewsDetailsWebView : WebView
    {
        private const string UpperHtmlLayout =
            @"<html><head>
            <style>pre{white-space:pre-wrap;white-space:-moz-pre-wrap;white-space:-pre-wrap;white-space:-o-pre-wrap;word-wrap:break-word;}</style>
            <style>div{max-width:100%;width:auto;height:auto}</style><style>img{display:inline;height:auto;max-width:100%}</style>
            <style>body{font-family:'Roboto'}</style>
            </head><body>";

        private const string DownHtmlLayout = "</body></html>";
        private string _text;

        public NewsDetailsWebView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

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