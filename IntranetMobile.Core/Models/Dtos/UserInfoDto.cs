using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    public class UserInfoDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string ServerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}