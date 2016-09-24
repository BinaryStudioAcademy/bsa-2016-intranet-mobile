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
using MvvmCross.Platform;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.Reviewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("TicketDetailsContent")]
    public sealed partial class NewTicketPage : BasePage
    {
        private DateTime _dateTime;
        private DateTimeOffset _date;
        private TimeSpan _time;
        public NewTicketPage()
        {
            this.InitializeComponent();
            DataContextChanged += (sender, args) =>
            {
                var vm = (NewTicketViewModel)DataContext;
                if (vm != null)
                {
                    vm.NavigateBack = () =>
                    {
                        ReviewerPage.Instance.ContentFrame.Navigate(typeof(EmptyPage));
                    };
                }
            };
            _date = new DateTime(DatePicker.Date.Year, DatePicker.Date.Month, DatePicker.Date.Day);
        }

        private void DatePicker_OnDateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            _date = e.NewDate;
            _dateTime = new DateTime(_date.Year,_date.Month,_date.Day,_time.Hours,_time.Minutes,_time.Seconds);
            ((NewTicketViewModel)DataContext).Date = new DateTime(_date.Year, _date.Month, _date.Day, _time.Hours, _time.Minutes, _time.Seconds);
        }

        private void TimePicker_OnTimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            _time = e.NewTime;
            ((NewTicketViewModel)DataContext).Date = new DateTime(_date.Year, _date.Month, _date.Day, _time.Hours, _time.Minutes, _time.Seconds);
        }
    }
}
