using System;

namespace IntranetMobile.Core.Models
{
    public class User : Persist
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public string AvatarUri { get; set; }

        public string PositionId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }

        public DateTime HireDate { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}