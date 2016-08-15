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

        private readonly RestClient _restClient;

        public NewsService(RestClient client)
        {
            _restClient = client;
        }

        public Task<List<CompNewsDto>> CompanyNews(int skip, int limit)
        {
            var compNewsReqParams = new CompNewsReqParams
            {
                type = Type,
                limit = limit,
                skip = skip
            };

            return _restClient.GetAsync<List<CompNewsDto>>("api/news", compNewsReqParams);
        }

        public Task<bool> LikeComment(string newsId, string commentId)
        {
            var resource = string.Format(LikeUnlikeCommentPath, newsId, commentId);

            var requestObject = new CompNewsLikeCommentDto
            {
                newsId = newsId,
                commentId = commentId
            };

            return _restClient.PostAsync(resource, requestObject);
        }

        public Task<bool> LikeNews(string newsId)
        {
            var resource = string.Format(LikeUnlikeNews, newsId);

            var requestObject = new CompNewsLikeNewsDto {id = newsId};

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
    }
}