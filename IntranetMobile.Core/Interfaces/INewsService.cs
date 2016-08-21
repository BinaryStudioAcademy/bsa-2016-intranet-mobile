using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface INewsService
    {
        Task<List<News>> GetNews(int skip, int limit);

        Task<List<WeeklyNews>> GetWeeklyNews(int skip, int limit);

        Task<News> GetNewsById(string newsId);

        WeeklyNews GetWeeklyNewsById(string newsId);

        Task<bool> LikeNews(string newsId);

        Task<bool> UnLikeNews(string newsId);

        Task<bool> LikeComment(string newsId, string commentId);

        Task<bool> UnlikeComment(string newsId, string commentId);

        Task<CommentsResponseDto> LoadListOfComments(string newsId);
 
        Task<bool> AddNewCommentRequest(string author, string body, string newsId);
    }
}