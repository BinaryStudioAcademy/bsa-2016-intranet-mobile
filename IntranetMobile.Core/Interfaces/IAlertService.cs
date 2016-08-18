using System;

namespace IntranetMobile.Core.Interfaces
{
    public interface IAlertService
    {
        void ShowMessage(string text);

        void ShowDialogBox(
            string text,
            string okButtonCaption,
            string cancelButtonCaption,
            Action okButtonAction);
    }
}