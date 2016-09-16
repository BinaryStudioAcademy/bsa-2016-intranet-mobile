using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.Users
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("MainContent")]
    public sealed partial class UsersPage : BasePage
    {
        public UsersPage()
        {
            InitializeComponent();
        }
    }
}