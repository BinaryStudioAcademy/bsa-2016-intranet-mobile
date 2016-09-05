using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.ViewModels.News;
using MvvmCross.Core.ViewModels;

namespace IntranetMobile.Core.ViewModels.Reviewer
{
    public class ReviewerViewModel : BaseViewModel
    {
        public ReviewerViewModel()
        {
            Title = "Reviewer";
        }

        public ReviewerSectionViewModel Cs { get; set; } = new ReviewerSectionViewModel("3");
        public ReviewerSectionViewModel Js { get; set; } = new ReviewerSectionViewModel("2");
        public ReviewerSectionViewModel Php { get; set; } = new ReviewerSectionViewModel("1");

    }
}
