using System;
using System.Collections.Generic;

namespace IntranetMobile.Core.Models
{
    public class News
    {
        public string NewsId { get; set; }

        public string AuthorId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public List<string> Likes { get; set; }

        public List<Comment> Comments { get; set; }
    }
}

