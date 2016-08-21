using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Droid.Services
{
    public class AndroidDeviceInfo : IDeviceInfo
    {
        public Platform CurrentPlatform => Platform.Android;
    }
}