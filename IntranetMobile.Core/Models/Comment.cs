using System;
using System.Collections.Generic;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class Comment
    {
        public string CommentId { get; set; }

        public string AuthorId { get; set; }

        public string Date { get; set; }

        public string Body { get; set; }

        public List<string> Likes { get; } = new List<string>();

        public Comment UpdateFromDto(CommentDto commentDto)
        {
            CommentId = commentDto.commentId;
            AuthorId = commentDto.authorId;
            Date = Convert.ToString(commentDto.date.UnixTimestampToDateTime());
            Body = commentDto.body;

            // Not recreating list in case of situation if somoene is holding list's reference during update
            Likes.Clear();
            Likes.AddRange(commentDto.likes);

            // For fluent interface purposes
            return this;
        }
    }
}