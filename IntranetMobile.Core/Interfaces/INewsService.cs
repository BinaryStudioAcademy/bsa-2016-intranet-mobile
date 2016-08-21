using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface INewsService
    {
        Task<List<News>> GetNewsAsync(int skip, int limit);

        Task<News> GetNewsByIdAsync(string newsId);

        Task<List<WeeklyNews>> GetWeeklyNewsAsync(int skip, int limit);

        WeeklyNews GetWeeklyNewsByIdAsync(string newsId);

        Task<bool> LikeNewsAsync(string newsId);

        Task<bool> UnLikeNewsAsync(string newsId);

        Task<bool> LikeCommentAsync(string newsId, string commentId);

        Task<bool> UnlikeCommentAsync(string newsId, string commentId);

        Task<bool> AddCommentAsync(string author, string body, string newsId);

        Task<NewsDto> LoadNewsByIdAsync(string newsId);

        Task<CommentsResponseDto> LoadListOfCommentsAsync(string newsId);
    }
}