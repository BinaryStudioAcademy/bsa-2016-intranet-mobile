using System;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using IntranetMobile.Core.Interfaces;
using MvvmCross.Platform.Droid.Platform;

namespace IntranetMobile.Droid.Services
{
    public class AlertService : IAlertService
    {
        private readonly IMvxAndroidCurrentTopActivity _topActivityHolder;

        public AlertService(IMvxAndroidCurrentTopActivity topActivityHolder)
        {
            _topActivityHolder = topActivityHolder;
        }

        public void ShowPopupMessage(string text)
        {
            var handler = new Handler(_topActivityHolder.Activity.MainLooper);
            handler.Post(() => Toast.MakeText(_topActivityHolder.Activity, text, ToastLength.Short).Show());
        }

        public void ShowMessageBox(string title, string text)
        {
            ShowAlertDialog(title, text, "OK", null, null);
        }

        public void ShowDialogBox(
            string title,
            string text,
            string okButtonCaption,
            string cancelButtonCaption,
            Action okButtonAction)
        {
            ShowAlertDialog(title, text, okButtonCaption, cancelButtonCaption, okButtonAction);
        }

        private void ShowAlertDialog(
            string title,
            string text,
            string okButtonCaption,
            string cancelButtonCaption,
            Action okButtonAction)
        {
            var handler = new Handler(_topActivityHolder.Activity.MainLooper);
            handler.Post(() =>
            {
                var builder = new AlertDialog.Builder(_topActivityHolder.Activity);
                builder
                    .SetTitle(title)
                    .SetMessage(text)
                    .SetPositiveButton(okButtonCaption, (sender, args) => okButtonAction?.Invoke())
                    .SetNegativeButton(cancelButtonCaption, (sender, args) => { })
                    .Show();
            });
        }
    }
}