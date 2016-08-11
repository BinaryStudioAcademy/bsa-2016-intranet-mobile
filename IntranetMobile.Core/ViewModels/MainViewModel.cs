using System.Collections.ObjectModel;
using System.Windows.Input;
using IntranetMobile.Core.ViewModels.Fragments;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public override void Start()
        {
            base.Start();

            ShowViewModel<MenuFragmentViewModel>();
            //ShowViewModel<NewsFragmentViewModel>();
        }
    }
}