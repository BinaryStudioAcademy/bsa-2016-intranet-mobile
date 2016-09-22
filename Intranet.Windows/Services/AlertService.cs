using System;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using IntranetMobile.Core.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Intranet.WindowsUWP.Services
{
    public class AlertService : IAlertService
    {
        public void ShowPopupMessage(string text)
        {
            var visual = new ToastVisual
            {
                BindingGeneric = new ToastBindingGeneric
                {
                    Children =
                    {
                        new AdaptiveText
                        {
                            Text = "Binary studio"
                        },
                        new AdaptiveText
                        {
                            Text = text
                        }
                    }
                }
            };

            var toastContent = new ToastContent
            {
                Visual = visual
            };

            var toast = new ToastNotification(toastContent.GetXml());

            ToastNotificationManager.CreateToastNotifier()
                .Show(toast);
        }

        public async void ShowMessageBox(string title, string text)
        {
            var dialog = new MessageDialog(text) {Title = title};
            await dialog.ShowAsync();
        }

        public async void ShowDialogBox(string title, string text, string okButtonCaption, string cancelButtonCaption,
            Action okButtonAction)
        {
            var dialog = new MessageDialog(text) {Title = title};
            dialog.Commands.Add(new UICommand {Label = okButtonCaption, Id = 0});
            dialog.Commands.Add(new UICommand {Label = cancelButtonCaption, Id = 1});
            var res = await dialog.ShowAsync();

            if ((int) res.Id == 0)
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