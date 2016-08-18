using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;
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

        private List<News> _companyNewsCache;
        private List<News> _weeklyNewsCache;

        private readonly RestClient _restClient;

        public NewsService(RestClient client)
        {
            _restClient = client;
        }

        public async Task<List<News>> GetCompanyNews(int skip, int limit)
        {
            var compNewsReqParams = new NewsReqParams
            {
                type = Type,
                limit = limit,
                skip = skip
            };

            var news = await _restClient.GetAsync<List<NewsDto>>("api/news", compNewsReqParams).ConfigureAwait(false);

            _companyNewsCache = news.Select(n => GetCompanyNewsFromDto(n))
                                    .ToList();

            return _companyNewsCache;
        }

        public Task<List<WeekNewsDto>> GetWeeklyNews(int skip, int limit)
        {
            var weekNewsReqParams = new WeekNewsReqParams
            {
                skip = skip,
                limit = limit,
                published = Published
            };

            return _restClient.GetAsync<List<WeekNewsDto>>("api/packs", weekNewsReqParams);
        }

        public async Task<News> GetCompanyNewsById(string newsId)
        {
            News result;

            if (_companyNewsCache != null && _companyNewsCache.Count > 0)
            {
                result = _companyNewsCache.FirstOrDefault(n => n.NewsId.Equals(newsId));
                if (result != null)
                    return result;
            }

            var dto = await _restClient.GetAsync<NewsDto>(string.Format(NewsByIdPath, newsId));
            result = GetCompanyNewsFromDto(dto);

            return result;
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

        private News GetCompanyNewsFromDto(NewsDto dto)
        {
            if (dto == null)
                return null;
            
            return new News
            {
                NewsId = dto.newsId,
                Title = dto.title,
                AuthorId = dto.authorId,
                Body = dto.body,
                Date = dto.date.UnixTimestampToDateTime(),
                Type = dto.type,
                Likes = dto.likes,
                Comments = dto.comments?.Select(c => new Comment
                {
                    AuthorId = c.authorId,
                    Body = c.body,
                    Date = c.date.UnixTimestampToDateTime(),
                    Likes = c.likes
                }).ToList()
            };
        }
    }
}