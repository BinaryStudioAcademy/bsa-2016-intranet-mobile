using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;

namespace Intranet.WindowsUWP.Services
{
    public class WindowsDeviceInfo : IDeviceInfo
    {
        public Platform CurrentPlatform => Platform.Windows;
    }
}
