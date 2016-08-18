using System.Collections.Generic;

namespace IntranetMobile.Core.Models.Dtos
{
    public class UserDto
    {
        public UserCv UserCv { get; set; }
        public UserPdp UserPdp { get; set; }
        public string Email { get; set; }
        public string ServerUserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public Avatar Avatar { get; set; }
        public string WorkDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool ChangeAccept { get; set; }
        public PreModeration PreModeration { get; set; }
        public List<AllPosition> AllPositions { get; set; }
        public List<AllDirection> AllDirections { get; set; }
        public string Position { get; set; }
        public string Direction { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string CurrentProject { get; set; }
        public string Id { get; set; }
    }

    public class UserCv
    {
        public List<object> Projects { get; set; }
        public bool IsDeleted { get; set; }
        public List<object> Technologies { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Id { get; set; }
    }

    public class UserPdp
    {
        public string Position { get; set; }
        public string Direction { get; set; }
        public List<object> Achievements { get; set; }
        public List<object> Certifications { get; set; }
        public List<object> CompletedCertifications { get; set; }
        public List<object> Tasks { get; set; }
        public List<object> Tests { get; set; }
        public List<object> Technologies { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Id { get; set; }
    }

    public class Avatar
    {
        public string UrlAva { get; set; }
        public string ThumbnailUrlAva { get; set; }
    }

    public class Avatar2
    {
        public string UrlAva { get; set; }
        public string ThumbnailUrlAva { get; set; }
    }

    public class PreModeration
    {
        public Avatar2 Avatar { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string WorkDate { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
    }

    public class AllPosition
    {
        public List<object> Pdps { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Id { get; set; }
    }

    public class AllDirection
    {
        public List<object> Pdps { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Id { get; set; }
    }
}