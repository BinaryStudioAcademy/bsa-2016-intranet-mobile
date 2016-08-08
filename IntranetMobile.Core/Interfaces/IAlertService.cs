using System;

namespace IntranetMobile.Core.Interfaces
{
    public interface IAlertService
    {
        void ShowMessage(string title, string text);

        void ShowDialogBox(string title,
                            string text,
                            string okButtonCaption,
                            string cancelButtonCaption,
                            Action okButtonAction);
    }
}
