using Windows.UI.Xaml.Controls;
using IntranetMobile.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.WindowsCommon.Platform;

namespace IntranetMobile.Universal
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame)
            : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Application();
        }
    }
}