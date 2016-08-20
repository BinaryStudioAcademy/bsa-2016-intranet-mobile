using System;
using IntranetMobile.Core.Interfaces;

namespace Intranet.WindowsUWP.Services
{
    public class WindowsLogger : ILogger
    {
        public void Error(Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"Intranet.Error. {e.ToString()}");
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Intranet.Error. {message}");
        }

        public void Error(string message, Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"Intranet.Error. {message} \n {e.ToString()}");
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Intranet.Info. {message}");
        }
    }
}
