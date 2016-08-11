using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
    public class CommentDto
    {
        [JsonProperty("author")]
        public string authorId { get; set; }

        public string body { get; set; }

        public long date { get; set; }

        [JsonProperty("_id")]
        public string commentId { get; set; }

        public List<string> likes { get; set; }
    }
}