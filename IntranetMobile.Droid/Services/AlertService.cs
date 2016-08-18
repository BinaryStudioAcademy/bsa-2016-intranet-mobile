using System;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
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

        public void ShowMessage(string text)
        {
            _handler.Post(() => Toast.MakeText(_applicationContext, text, ToastLength.Short).Show());
        }

        public void ShowDialogBox(
            string text,
            string okButtonCaption,
            string cancelButtonCaption,
            Action okButtonAction)
        {
            _handler.Post(() =>
            {
                var builder = new AlertDialog.Builder(_applicationContext);
                builder
                    .SetMessage(text)
                    .SetPositiveButton(okButtonCaption, (sender, args) => okButtonAction?.Invoke())
                    .SetNegativeButton(cancelButtonCaption, (sender, args) => { })
                    .Show();
            });
        }
    }
}