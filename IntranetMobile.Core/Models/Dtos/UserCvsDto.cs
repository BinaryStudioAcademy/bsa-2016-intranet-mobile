using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    public class CategoryCvsDto
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

    public class TechnologyCvsDto
    {
        [JsonProperty("category")]
        public CategoryCvsDto Category { get; set; }

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

        [JsonProperty("stars")]
        public string Stars { get; set; }
    }

    public class UserCvCvsDto
    {
        [JsonProperty("projects")]
        public object[] Projects { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("technologies")]
        public TechnologyCvsDto[] Technologies { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AvatarCvsDto
    {
        [JsonProperty("urlAva")]
        public string UrlAva { get; set; }

        [JsonProperty("thumbnailUrlAva")]
        public string ThumbnailUrlAva { get; set; }
    }

    public class Avatar2CvsDto
    {
        [JsonProperty("urlAva")]
        public string UrlAva { get; set; }

        [JsonProperty("thumbnailUrlAva")]
        public string ThumbnailUrlAva { get; set; }
    }

    public class PreModerationCvsDto
    {
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("avatar")]
        public Avatar2CvsDto Avatar { get; set; }
    }

    public class CompletedCertificationCvsDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Technology2CvsDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class PdpCvsDto
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public object[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public object[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertificationCvsDto[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public object[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology2CvsDto[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AllDirectionCvsDto
    {
        [JsonProperty("pdps")]
        public PdpCvsDto[] Pdps { get; set; }

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

    public class CompletedCertification2CvsDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Technology3CvsDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Pdp2CvsDto
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("achievements")]
        public object[] Achievements { get; set; }

        [JsonProperty("certifications")]
        public object[] Certifications { get; set; }

        [JsonProperty("completedCertifications")]
        public CompletedCertification2CvsDto[] CompletedCertifications { get; set; }

        [JsonProperty("tasks")]
        public object[] Tasks { get; set; }

        [JsonProperty("tests")]
        public object[] Tests { get; set; }

        [JsonProperty("technologies")]
        public Technology3CvsDto[] Technologies { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AllPositionCvsDto
    {
        [JsonProperty("pdps")]
        public Pdp2CvsDto[] Pdps { get; set; }

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

    public class UserCvsDto
    {
        [JsonProperty("userCV")]
        public UserCvCvsDto UserCv { get; set; }

        [JsonProperty("userPDP")]
        public string UserPdp { get; set; }

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
        public AvatarCvsDto Avatar { get; set; }

        [JsonProperty("workDate")]
        public string WorkDate { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("changeAccept")]
        public bool ChangeAccept { get; set; }

        [JsonProperty("preModeration")]
        public PreModerationCvsDto PreModeration { get; set; }

        [JsonProperty("allDirections")]
        public AllDirectionCvsDto[] AllDirections { get; set; }

        [JsonProperty("allPositions")]
        public AllPositionCvsDto[] AllPositions { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}