using System;
using Android.App;
using Android.Renderscripts;
using Android.Text.Format;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core;
using IntranetMobile.Core.Extensions;
using static Android.App.DatePickerDialog;
using static Android.App.TimePickerDialog;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "NewTicketActivity",
              Theme = "@style/BSTheme")]
    public class NewTicketActivity : BaseToolbarActivity<NewTicketViewModel>, IOnDateSetListener, IOnTimeSetListener
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_new_ticket;

        private TextView dateTextView;
        private TextView timeTextView;        

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            dateTextView = FindViewById<TextView>(Resource.Id.datepickerTextView);
            timeTextView = FindViewById<TextView>(Resource.Id.timepickerTextView);

            dateTextView.Click += delegate
            {
                var dialog = new DatePickerDialog(this, this, DateTime.Today.Year, DateTime.Today.Month-1, DateTime.Today.Day);
                dialog.Show();
            };

            timeTextView.Click += delegate
            {
                var dialog = new TimePickerDialog(this, this, DateTime.Today.Hour, DateTime.Today.Minute, true);
                dialog.Show();
            };
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            var date = new DateTime(year, monthOfYear, dayOfMonth);
            dateTextView.Text = date.ToDateString();

            ViewModel.Date = new DateTime(year, monthOfYear, dayOfMonth, ViewModel.Date.Hour, ViewModel.Date.Minute, 0);
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            timeTextView.Text = hourOfDay + " : " + minute;

            ViewModel.Date = new DateTime(ViewModel.Date.Year, ViewModel.Date.Month, ViewModel.Date.Day, hourOfDay, minute, 0);
        }
    }
}

