using System;
using System.Collections.Generic;
using System.Linq;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models.Dtos;

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

        public List<string> Likes { get; } = new List<string>();

        public List<string> Comments { get; } = new List<string>();

        public News UpdateFromDto(NewsDto newsDto)
        {
            NewsId = newsDto.newsId;
            AuthorId = newsDto.authorId;
            Title = newsDto.title;
            Body = newsDto.body;
            Date = newsDto.date.UnixTimestampToDateTime();
            Type = newsDto.type;

            // Not recreating list in case of situation if somoene is holding list's reference during update
            Likes.Clear();
            Likes.AddRange(newsDto.likes);

            // Same thing here
            Comments.Clear();
            Comments.AddRange(newsDto.comments.Select(commentDto => commentDto.commentId));

            // For fluent interface purposes
            return this;
        }
    }
}