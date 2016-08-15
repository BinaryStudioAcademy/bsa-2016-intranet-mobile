﻿using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface INewsService
    {
        Task<List<CompNewsDto>> CompanyNews(int skip, int limit);

        Task<List<WeekNewsDto>> Weeklies(int skip, int limit);

        Task<bool> LikeNews(string newsId);

        Task<bool> UnLikeNews(string newsId);

        Task<bool> LikeComment(string newsId, string commentId);

        Task<bool> UnlikeComment(string newsId, string commentId);
    }
}