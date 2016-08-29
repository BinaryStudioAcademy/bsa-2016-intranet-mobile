using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetMobile.Core.ViewModels.Profile
{
    public class UserCertificationViewModel : BaseViewModel
    {
        private string _name;
        private string _category;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; 
                RaisePropertyChanged(()=> Name);
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                RaisePropertyChanged(()=> Category);
            }
        }
    }
}
