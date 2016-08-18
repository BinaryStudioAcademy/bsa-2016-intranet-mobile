using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Services
{
    public class NewsService : INewsService
    {
        private const string Type = "company";
        private const string Published = "yes";
        private const string LikeUnlikeCommentPath = "api/news/{0}/comments/{1}/likes";
        private const string LikeUnlikeNews = "api/news/{0}/likes";
        private const string NewsByIdPath = "api/news/{0}/";

        private readonly RestClient _restClient;

        public NewsService(RestClient client)
        {
            _restClient = client;
        }

        public Task<List<NewsDto>> CompanyNews(int skip, int limit)
        {
            var compNewsReqParams = new NewsReqParams
            {
                type = Type,
                limit = limit,
                skip = skip
            };

            return _restClient.GetAsync<List<NewsDto>>("api/news", compNewsReqParams);
        }

        public Task<bool> LikeComment(string newsId, string commentId)
        {
            var resource = string.Format(LikeUnlikeCommentPath, newsId, commentId);

            var requestObject = new NewsCommentLikeDto
            {
                newsId = newsId,
                commentId = commentId
            };

            return _restClient.PostAsync(resource, requestObject);
        }

        public Task<bool> LikeNews(string newsId)
        {
            var resource = string.Format(LikeUnlikeNews, newsId);

            var requestObject = new NewsLikeDto {id = newsId};

            return _restClient.PostAsync(resource, requestObject);
        }

        public Task<bool> UnlikeComment(string newsId, string commentId)
        {
            var resource = string.Format(LikeUnlikeCommentPath, newsId, commentId);

            return _restClient.DeleteAsync(resource);
        }

        public Task<bool> UnLikeNews(string id)
        {
            var resource = string.Format(LikeUnlikeNews, id);

            return _restClient.DeleteAsync(resource);
        }

        public Task<List<WeekNewsDto>> Weeklies(int skip, int limit)
        {
            var weekNewsReqParams = new WeekNewsReqParams
            {
                skip = skip,
                limit = limit,
                published = Published
            };

            return _restClient.GetAsync<List<WeekNewsDto>>("api/packs", weekNewsReqParams);
        }

        public Task<NewsDto> GetNewsByIdAsync(string newsId)
        {
            return _restClient.GetAsync<NewsDto>(string.Format(NewsByIdPath, newsId));
        }

        public Task<List<CommentDto>> GetListOfComments(string newsId)
        {
            var resource = string.Format(NewsByIdPath, newsId) + "comments";

            return _restClient.GetAsync<List<CommentDto>>(resource);
        }

        public Task<bool> AddNewCommentRequest(string author, string body, string newsId)
        {
            var resource = string.Format(NewsByIdPath, newsId) + "comments";
            var comment = new CommentRequestDto();

            comment.Push.authorId = author;
            comment.Push.body = body;
            comment.Push.date = DateTime.Now.Millisecond;

            return _restClient.PostAsync<bool>(resource, comment);
        }
    }
}