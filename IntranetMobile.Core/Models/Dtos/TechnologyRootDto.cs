using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    public class CategoryRootDto
    {
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

    public class TechnologyRootDto
    {
        [JsonProperty("category")]
        public CategoryRootDto Category { get; set; }

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
}