using System;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
    }
}