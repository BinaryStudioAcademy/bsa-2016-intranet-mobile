using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IntranetMobile.Core.ViewModels.Reviewer;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.Reviewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("TicketDetailsContent")]
    public sealed partial class TicketDetailsPage : BasePage
    {
        public TicketDetailsPage()
        {
            this.InitializeComponent();
            DataContextChanged += (sender, args) =>
            {
                var vm = ViewModel as TicketDetailsViewModel;
                if (vm != null)
                {
                    vm.NotifyItemDeleted = ticketId =>
                    {
                        if (ReviewerPage.Instance != null && ReviewerPage.Instance.ContentFrame != null)
                        {
                            ReviewerPage.Instance.ContentFrame.Navigate(typeof(EmptyPage));
                            ReviewerPage.Instance.RefreshContent();
                        }
                    };
                }
            };
        }
    }
}
