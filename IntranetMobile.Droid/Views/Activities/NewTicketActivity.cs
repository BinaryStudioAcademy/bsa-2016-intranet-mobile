using System;
using Android.App;
using Android.Renderscripts;
using Android.Text.Format;
using Android.Views;
using Android.Widget;
using IntranetMobile.Core;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.Reviewer;
using static Android.App.DatePickerDialog;
using static Android.App.TimePickerDialog;

namespace IntranetMobile.Droid.Views.Activities
{
    [Activity(Label = "NewTicketActivity",
              Theme = "@style/BSTheme")]
    public class NewTicketActivity : BaseToolbarActivity<NewTicketViewModel>, IOnDateSetListener, IOnTimeSetListener
    {
        public override int ActivityLayout { get; } = Resource.Layout.activity_new_ticket;

        private TextView _dateTextView;
        private TextView _timeTextView;        

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ViewModel.NavigateBack = () => OnBackPressed();

            var radioListener = new RadioListener();
            radioListener.ViewModel = ViewModel;

            var radioNet = FindViewById<RadioButton>(Resource.Id.radioNet);
            var radioJs = FindViewById<RadioButton>(Resource.Id.radioJs);
            var radioPhp = FindViewById<RadioButton>(Resource.Id.radioPhp);

            radioNet.SetOnCheckedChangeListener(radioListener);
            radioJs.SetOnCheckedChangeListener(radioListener);
            radioPhp.SetOnCheckedChangeListener(radioListener);

            _dateTextView = FindViewById<TextView>(Resource.Id.datepickerTextView);
            _timeTextView = FindViewById<TextView>(Resource.Id.timepickerTextView);

            _dateTextView.Click += delegate
            {
                var dialog = new DatePickerDialog(this, this, ViewModel.Date.Year, ViewModel.Date.Month-1, ViewModel.Date.Day);
                dialog.Show();
            };

            _timeTextView.Click += delegate
            {
                var dialog = new TimePickerDialog(this, this, ViewModel.Date.Hour, ViewModel.Date.Minute, true);
                dialog.Show();
            };

            _dateTextView.Text = ViewModel.Date.ToDateString();
            SetTimeLabel(ViewModel.Date.Hour, ViewModel.Date.Minute);
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            ViewModel.Date = new DateTime(year, monthOfYear + 1, dayOfMonth, ViewModel.Date.Hour, ViewModel.Date.Minute, 0);
            _dateTextView.Text = ViewModel.Date.ToDateString();
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            ViewModel.Date = new DateTime(ViewModel.Date.Year, ViewModel.Date.Month, ViewModel.Date.Day, hourOfDay, minute, 0);
            SetTimeLabel(hourOfDay, minute);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_ticket_add_accept:
                    {
                        ViewModel.CreateTicketCommand.Execute(null);
                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_ticket_add, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void SetTimeLabel(int hour, int minute)
        {
            string hourStr = hour > 9 ? hour.ToString() : "0" + hour;
            string minuteStr = minute > 9 ? minute.ToString() : "0" + minute;
            _timeTextView.Text = hourStr + " : " + minuteStr;
        }

        private class RadioListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
        {
            public NewTicketViewModel ViewModel { get; set; }

            public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
            {
                if (ViewModel == null)
                    return;
                    
                var id = buttonView.Id;
                switch (id)
                {
                    case Resource.Id.radioNet:
                        ViewModel.Group = ReviewerGroup.DotNet;
                        break;
                    case Resource.Id.radioJs:
                        ViewModel.Group = ReviewerGroup.JavaScript;
                        break;
                    case Resource.Id.radioPhp:
                        ViewModel.Group = ReviewerGroup.Php;
                        break;
                    default:
                        return;
                }
            }
        }
    }
}

