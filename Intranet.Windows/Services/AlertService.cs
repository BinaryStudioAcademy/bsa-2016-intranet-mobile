using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;

namespace Intranet.WindowsUWP.Services
{
    public class AlertService : IAlertService
    {
        public void ShowPopupMessage(string text)
        {
            throw new NotImplementedException();
        }

        public void ShowMessageBox(string title, string text)
        {
            throw new NotImplementedException();
        }

        public void ShowDialogBox(string title, string text, string okButtonCaption, string cancelButtonCaption, Action okButtonAction)
        {
            throw new NotImplementedException();
        }
    }
}
