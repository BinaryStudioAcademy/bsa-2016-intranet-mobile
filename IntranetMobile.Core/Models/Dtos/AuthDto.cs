// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class AuthDto : Persist
    {
        public bool success { get; set; }

        public string message { get; set; }

        public string token { get; set; }
    }
}