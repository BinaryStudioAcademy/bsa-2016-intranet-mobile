using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Models.Dtos
{
    public class AuthDto
    {
        public bool success { get; set; }

        public string message { get; set; }

        public string token { get; set; }
    }
}
