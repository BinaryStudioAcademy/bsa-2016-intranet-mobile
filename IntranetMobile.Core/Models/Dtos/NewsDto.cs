using System.Collections.Generic;
using Newtonsoft.Json;
using SQLiteNetExtensions.Attributes;

// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class NewsDto : Persist
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

        [TextBlob("RestrictIdsBlobbed")]
        public List<string> restrict_ids { get; set; }

        [TextBlob("AccessRolesBlobbed")]
        public List<string> access_roles { get; set; }

        [TextBlob("LikesBlobbed")]
        public List<string> likes { get; set; }

        [TextBlob("CommentsBlobbed")]
        public List<CommentDto> comments { get; set; }
    }
}