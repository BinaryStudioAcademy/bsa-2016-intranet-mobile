using System;
using Windows.UI.Popups;
using IntranetMobile.Core.Interfaces;

namespace Intranet.WindowsUWP.Services
{
    public class AlertService : IAlertService
    {
        public void ShowPopupMessage(string text)
        {
            throw new NotImplementedException();
        }

        public async void ShowMessageBox(string title, string text)
        {
            var dialog = new MessageDialog(text) { Title = title };
            await dialog.ShowAsync();
        }

        public async void ShowDialogBox(string title, string text, string okButtonCaption, string cancelButtonCaption, Action okButtonAction)
        {
            var dialog = new MessageDialog(text) { Title = title };
            dialog.Commands.Add(new UICommand { Label = okButtonCaption, Id = 0 });
            dialog.Commands.Add(new UICommand { Label = cancelButtonCaption, Id = 1 });
            var res = await dialog.ShowAsync();

            if ((int)res.Id == 0)
            {
                okButtonAction?.Invoke();
            }
        }

        public void ShowConnectionLostMessage()
        {
            ShowPopupMessage("Internet connection unavailable");
        }
    }
}
