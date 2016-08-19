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

        private readonly RestClient _restClient;

        private List<News> _companyNewsCache;
        private List<WeeklyNews> _weeklyNewsCache;

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

            _companyNewsCache = news.Select(GetCompanyNewsFromDto).ToList();

            return _companyNewsCache;
        }

        public async Task<List<WeeklyNews>> GetWeeklyNews(int skip, int limit)
        {
            var weekNewsReqParams = new WeekNewsReqParams
            {
                skip = skip,
                limit = limit,
                published = Published
            };

            var news = await _restClient.GetAsync<List<WeekNewsDto>>("api/packs", weekNewsReqParams);

            _weeklyNewsCache = news.OrderByDescending(n => n.date).Select(GetWeeklyNewsFromDto).ToList();
            return _weeklyNewsCache;
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

        public WeeklyNews GetWeeklyNewsById(string newsId)
        {
            WeeklyNews result;

            if (_weeklyNewsCache != null && _weeklyNewsCache.Count > 0)
            {
                result = _weeklyNewsCache.FirstOrDefault(n => n.WeeklyId.Equals(newsId));
                if (result != null)
                    return result;
            }

            return null;
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
            {
                return null;
            }

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

        public async Task<CommentsResponseDto> GetListOfComments(string newsId)
        {
            var resource = string.Format(NewsByIdPath, newsId) + "comments";



            return await _restClient.GetAsync<CommentsResponseDto>(resource);
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

        private WeeklyNews GetWeeklyNewsFromDto(WeekNewsDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new WeeklyNews
            {
                WeeklyId = dto.weekliesId,
                Title = dto.title,
                AuthorId = dto.authorId,
                Date = dto.date.UnixTimestampToDateTime(),
                SubNewsIdList = dto.news,
                FullNews = dto.fullNews != null
                              ? dto.fullNews.Select(GetCompanyNewsFromDto).ToList()
                              : null
            };
        }
    }
}