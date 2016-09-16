using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.News
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("CompanyNewsContent")]
    public sealed partial class NewsDetailsPage : BasePage
    {
        public NewsDetailsPage()
        {
            InitializeComponent();
        }
    }
}