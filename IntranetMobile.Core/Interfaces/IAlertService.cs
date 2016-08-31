using System;

namespace IntranetMobile.Core.Interfaces
{
    public interface IAlertService
    {
        void ShowPopupMessage(string text);

        void ShowMessageBox(string title, string text);

        void ShowDialogBox(
            string title,
            string text,
            string okButtonCaption,
            string cancelButtonCaption,
            Action okButtonAction);

        void ShowConnectionLostMessage();
    }
}