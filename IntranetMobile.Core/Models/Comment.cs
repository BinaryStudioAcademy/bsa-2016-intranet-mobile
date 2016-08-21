using System;
using System.Collections.Generic;

namespace IntranetMobile.Core.Models
{
    public class Comment
    {
        public string CommentId { get; set; }

        public string AuthorId { get; set; }

        public DateTime Date { get; set; }

        public string Body { get; set; }

        public List<string> Likes { get; set; }

        public string LikeImageViewUrl { get; set; }
    }
}