using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core
{
    public class CommentRequestDto
    {
        [JsonProperty("$push")]
        public CommentRequest Push { get; set; }


        public class CommentRequest
        {
            public Comment comments { get; set; }


            public class Comment
            {
                public string author { get; set; }

                public string body { get; set; }

                public long date { get; set; }

                public List<string> likes { get; set; }
            }
        }
    }
}

