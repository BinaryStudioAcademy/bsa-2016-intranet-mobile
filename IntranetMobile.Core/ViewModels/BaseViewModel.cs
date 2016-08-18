using System;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public virtual string Title { get; protected set; } = string.Empty;
    }
}