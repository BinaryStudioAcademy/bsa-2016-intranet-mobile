using System;
using IntranetMobile.Core.Models.Dtos;
using Newtonsoft.Json;

namespace IntranetMobile.Core
{
    public class CommentRequestDto
    {
        [JsonProperty("$push")]
        public CommentDto Push { get; set; }
    }
}

