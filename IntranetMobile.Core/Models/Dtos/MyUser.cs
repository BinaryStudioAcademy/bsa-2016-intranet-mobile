namespace IntranetMobile.Core.Models.Dtos
{
    public class MyUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Iat { get; set; }
        public string LocalRole { get; set; }
    }
}