using SQLite.Net.Attributes;

namespace IntranetMobile.Core.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}