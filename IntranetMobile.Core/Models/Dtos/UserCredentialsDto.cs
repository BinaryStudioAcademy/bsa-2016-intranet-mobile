// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class UserCredentialsDto : Persist
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}