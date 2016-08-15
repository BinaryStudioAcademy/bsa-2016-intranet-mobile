using System;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Droid.Services
{
    public class AlertService : IAlertService
    {
        public void ShowMessage(string title, string text)
        {
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