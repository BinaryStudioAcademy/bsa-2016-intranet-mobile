using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    // TODO: Merge FULLY identical calsses
    public class TechnologyDto
    {
        [JsonProperty("userTech")]
        public string UserTech { get; set; }

        [JsonProperty("stars")]
        public object Stars { get; set; }
    }

    public class UserCvDto
    {
        [JsonProperty("projects")]
        public object[] Projects { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("technologies")]
        public TechnologyDto[] Technologies { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CategoryDto
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

    public class AchievementDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public CategoryDto Category { get; set; }
    }

    public class CertificationDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Category2Dto
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

    public class CompletedCertificationDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category2Dto Category { get; set; }
    }

    public class TaskDto
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Technology2Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UserPdpDto
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public AchievementDto[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public CertificationDto[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertificationDto[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public TaskDto[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology2Dto[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AvatarDto
    {
        [JsonProperty("urlAva")]
        public string UrlAva { get; set; }

        [JsonProperty("thumbnailUrlAva")]
        public string ThumbnailUrlAva { get; set; }
    }

    public class Avatar2Dto
    {
        [JsonProperty("urlAva")]
        public string UrlAva { get; set; }

        [JsonProperty("thumbnailUrlAva")]
        public string ThumbnailUrlAva { get; set; }
    }

    public class PreModerationDto
    {
        [JsonProperty("avatar")]
        public Avatar2Dto Avatar { get; set; }

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

    public class Category3Dto
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

    public class Achievement2Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category3Dto Category { get; set; }
    }

    public class Certification2Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Category4Dto
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

    public class CompletedCertification2Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category4Dto Category { get; set; }
    }

    public class Task2Dto
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Technology3Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class PdpDto
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public Achievement2Dto[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public Certification2Dto[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertification2Dto[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public Task2Dto[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology3Dto[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AllPositionDto
    {
        [JsonProperty("pdps")]
        public PdpDto[] Pdps { get; set; }

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

    public class Category5Dto
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

    public class Achievement3Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category5Dto Category { get; set; }
    }

    public class Certification3Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Category6Dto
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

    public class CompletedCertification3Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("category")]
        public Category6Dto Category { get; set; }
    }

    public class Task3Dto
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Technology4Dto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Pdp2Dto
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public Achievement3Dto[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public Certification3Dto[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertification3Dto[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public Task3Dto[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology4Dto[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AllDirectionDto
    {
        [JsonProperty("pdps")]
        public Pdp2Dto[] Pdps { get; set; }

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
        public UserCvDto UserCv { get; set; }

        [JsonProperty("userPDP")]
        public UserPdpDto UserPdp { get; set; }

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
        public AvatarDto Avatar { get; set; }

        [JsonProperty("workDate")]
        public string WorkDate { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("changeAccept")]
        public bool ChangeAccept { get; set; }

        [JsonProperty("preModeration")]
        public PreModerationDto PreModeration { get; set; }

        [JsonProperty("allPositions")]
        public AllPositionDto[] AllPositions { get; set; }

        [JsonProperty("allDirections")]
        public AllDirectionDto[] AllDirections { get; set; }

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