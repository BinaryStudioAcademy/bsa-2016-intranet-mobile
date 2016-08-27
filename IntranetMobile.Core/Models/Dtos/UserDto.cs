using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    public class Technology
    {
        [JsonProperty("userTech")]
        public string UserTech { get; set; }

        [JsonProperty("stars")]
        public object Stars { get; set; }
    }

    public class UserCv
    {
        [JsonProperty("projects")]
        public object[] Projects { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("technologies")]
        public Technology[] Technologies { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Category
    {
        [JsonProperty("achievements")]
        public object[] Achievements { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Achievement
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }
    }

    public class Certification
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Category2
    {
        [JsonProperty("certifications")]
        public object[] Certifications { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CompletedCertification
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category2 Category { get; set; }
    }

    public class Task
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Technology2
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UserPdp
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public Achievement[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public Certification[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertification[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public Task[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology2[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Avatar
    {
        [JsonProperty("urlAva")]
        public string UrlAva { get; set; }

        [JsonProperty("thumbnailUrlAva")]
        public string ThumbnailUrlAva { get; set; }
    }

    public class Avatar2
    {
        [JsonProperty("urlAva")]
        public string UrlAva { get; set; }

        [JsonProperty("thumbnailUrlAva")]
        public string ThumbnailUrlAva { get; set; }
    }

    public class PreModeration
    {
        [JsonProperty("avatar")]
        public Avatar2 Avatar { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("workDate")]
        public string WorkDate { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }

    public class Category3
    {
        [JsonProperty("achievements")]
        public object[] Achievements { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Achievement2
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category3 Category { get; set; }
    }

    public class Certification2
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Category4
    {
        [JsonProperty("certifications")]
        public object[] Certifications { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CompletedCertification2
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category4 Category { get; set; }
    }

    public class Task2
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Technology3
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Pdp
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public Achievement2[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public Certification2[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertification2[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public Task2[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology3[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AllPosition
    {
        [JsonProperty("pdps")]
        public Pdp[] Pdps { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Category5
    {
        [JsonProperty("achievements")]
        public object[] Achievements { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Achievement3
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category5 Category { get; set; }
    }

    public class Certification3
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Category6
    {
        [JsonProperty("certifications")]
        public object[] Certifications { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CompletedCertification3
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category6 Category { get; set; }
    }

    public class Task3
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Technology4
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Pdp2
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public Achievement3[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public Certification3[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertification3[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public Task3[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology4[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AllDirection
    {
        [JsonProperty("pdps")]
        public Pdp2[] Pdps { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class UserDto
    {
        [JsonProperty("userCV")]
        public UserCv UserCv { get; set; }

        [JsonProperty("userPDP")]
        public UserPdp UserPdp { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("serverUserId")]
        public string ServerUserId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        [JsonProperty("workDate")]
        public string WorkDate { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("changeAccept")]
        public bool ChangeAccept { get; set; }

        [JsonProperty("preModeration")]
        public PreModeration PreModeration { get; set; }

        [JsonProperty("allPositions")]
        public AllPosition[] AllPositions { get; set; }

        [JsonProperty("allDirections")]
        public AllDirection[] AllDirections { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("currentProject")]
        public string CurrentProject { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}