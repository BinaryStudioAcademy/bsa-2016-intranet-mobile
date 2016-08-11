using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface INewsService
    {
        Task<List<CompNewsDto>> CompanyNews(int skip, int limit);

        Task<List<WeekNewsDto>> Weeklies(int skip, int limit);

        Task LikeNews(string id);

        Task UnLikeNews(string id);

        Task LikeComment(string newsId, string commentId);

        Task UnlikeComment(string newsId, string CommentId);
    }
}