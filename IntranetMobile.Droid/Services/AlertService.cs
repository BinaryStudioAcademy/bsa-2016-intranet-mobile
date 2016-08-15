using System;
using Android.Content;
using Android.OS;
using Android.Widget;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Droid.Services
{
    public class AlertService : IAlertService
    {
        private readonly Context _applicationContext;
        private readonly Handler _handler;

        public AlertService(Context applicationContext)
        {
            _applicationContext = applicationContext;
            _handler = new Handler(_applicationContext.MainLooper);
        }

        public void ShowMessage(string title, string text)
        {
            _handler.Post(() => Toast.MakeText(_applicationContext, text, ToastLength.Long).Show());
        }

        public void ShowDialogBox(string title,
            string text,
            string okButtonCaption,
            string cancelButtonCaption,
            Action okButtonAction)
        {
        }
    }
}