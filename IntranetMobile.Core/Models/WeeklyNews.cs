using System;
using System.Collections.Generic;
using System.Linq;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class WeeklyNews
    {
        public string WeeklyId { get; set; }

        public string AuthorId { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public List<string> FullNews { get; } = new List<string>();

        public WeeklyNews UpdateFromDto(WeekNewsDto weekNewsDto)
        {
            WeeklyId = weekNewsDto.weekliesId;
            AuthorId = weekNewsDto.authorId;
            Title = weekNewsDto.title;
            Date = weekNewsDto.date.UnixTimestampToDateTime();

            // Not recreating list in case of situation if somoene is holding list's reference during update
            FullNews.Clear();
            FullNews.AddRange(weekNewsDto.fullNews.Select(newsDto => newsDto.newsId));

            // For fluent interface purposes
            return this;
        }
    }
}