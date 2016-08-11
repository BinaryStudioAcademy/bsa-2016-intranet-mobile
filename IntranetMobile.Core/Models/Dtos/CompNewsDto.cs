using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    public class CompNewsDto
    {
        [JsonProperty("_id")]
        public string newsId { get; set; }

        [JsonProperty("author")]
        public string authorId { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public long date { get; set; }

        public string type { get; set; }

        public int __v { get; set; }

        public List<string> restrict_ids { get; set; }

        public List<string> access_roles { get; set; }

        public List<string> likes { get; set; }

        public List<CommentDto> comments { get; set; }
    }
}