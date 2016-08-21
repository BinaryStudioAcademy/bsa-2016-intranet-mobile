using Windows.UI.Xaml.Media.Animation;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("LoginFieldsFrame")]
    public sealed partial class LoginLoadingPage : BasePage
    {
        public LoginLoadingPage()
        {
            this.InitializeComponent();
        }
    }
}
