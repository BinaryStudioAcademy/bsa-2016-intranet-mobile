﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private const string LikeUnlikeNewsPath = "api/news/{0}/likes";
        private const string NewsByIdPath = "api/news/{0}/";

        private readonly List<News> _newsCache = new List<News>();

        private readonly RestClient _restClient;
        private readonly List<WeeklyNews> _weeklyNewsCache = new List<WeeklyNews>();

        public NewsService(RestClient client)
        {
            _restClient = client;
        }

        public async Task<List<News>> GetNewsAsync(int skip, int limit)
        {
            var compNewsReqParams = new NewsReqParams
            {
                type = Type,
                limit = limit,
                skip = skip
            };

            var news = await _restClient.GetAsync<List<NewsDto>>("api/news", compNewsReqParams).ConfigureAwait(false);

            var newArrival = false;
            foreach (var newsDto in news)
            {
                var existingNews = _newsCache.FirstOrDefault(n => n.NewsId == newsDto.newsId);
                if (existingNews != null)
                {
                    existingNews.UpdateFromDto(newsDto);
                }
                else
                {
                    _newsCache.Add(new News().UpdateFromDto(newsDto));
                    newArrival = true;
                }
            }

            if (newArrival)
            {
                SortNewsCache();
            }

            return _newsCache;
        }

        public async Task<News> GetNewsByIdAsync(string newsId)
        {
            News result;

            if (_newsCache.Count > 0)
            {
                result = _newsCache.FirstOrDefault(n => n.NewsId.Equals(newsId));
                if (result != null)
                {
                    return result;
                }
            }

            var dto = await LoadNewsByIdAsync(newsId);
            result = new News().UpdateFromDto(dto);
            _newsCache.Add(result);

            // Possibly sort is not needed here
            SortNewsCache();

            return result;
        }

        public async Task<List<WeeklyNews>> GetWeeklyNewsAsync(int skip, int limit)
        {
            var weekNewsReqParams = new WeekNewsReqParams
            {
                skip = skip,
                limit = limit,
                published = Published
            };

            var news = await _restClient.GetAsync<List<WeekNewsDto>>("api/packs", weekNewsReqParams);

            var newArrival = false;
            foreach (var weekNewsDto in news)
            {
                var existingWeeklyNews = _weeklyNewsCache.FirstOrDefault(n => n.WeeklyId == weekNewsDto.weekliesId);
                if (existingWeeklyNews != null)
                {
                    existingWeeklyNews.UpdateFromDto(weekNewsDto);
                }
                else
                {
                    _weeklyNewsCache.Add(new WeeklyNews().UpdateFromDto(weekNewsDto));
                    newArrival = true;
                }
            }

            if (newArrival)
            {
                // OrderBy is dropped due to collection recreating
                _weeklyNewsCache.Sort((n1, n2) => n2.Date.CompareTo(n1.Date));
            }

            return _weeklyNewsCache;
        }

        public WeeklyNews GetWeeklyNewsByIdAsync(string newsId)
        {
            if (_weeklyNewsCache != null && _weeklyNewsCache.Count > 0)
            {
                var result = _weeklyNewsCache.FirstOrDefault(n => n.WeeklyId.Equals(newsId));
                if (result != null)
                {
                    return result;
                }
            }

            // TODO: Update weekly cache from server

            return null;
        }

        public async Task<bool> LikeNewsAsync(string newsId)
        {
            var resource = string.Format(LikeUnlikeNewsPath, newsId);

            var requestObject = new NewsLikeDto {id = newsId};

            var result = await _restClient.PostAsync(resource, requestObject);

            if (result)
            {
                // Null check is not used, it's desired that news will exist in cache already
                _newsCache.FirstOrDefault(news => news.NewsId == newsId)
                    .UpdateFromDto(await LoadNewsByIdAsync(newsId));
                // It is also possible to add like by hands, but it is better to update whole news.
            }

            return result;
        }

        public async Task<bool> UnLikeNewsAsync(string newsId)
        {
            var resource = string.Format(LikeUnlikeNewsPath, newsId);

            var result = await _restClient.DeleteAsync(resource);

            if (result)
            {
                // Null check is not used, it's desired that news will exist in cache already
                _newsCache.FirstOrDefault(news => news.NewsId == newsId)
                    .UpdateFromDto(await LoadNewsByIdAsync(newsId));
                // It is also possible to add like by hands, but it is better to update whole news.
            }

            return result;
        }

        public async Task<bool> LikeCommentAsync(string newsId, string commentId)
        {
            var resource = string.Format(LikeUnlikeCommentPath, newsId, commentId);

            var requestObject = new NewsCommentLikeDto
            {
                newsId = newsId,
                commentId = commentId
            };

            var result = await _restClient.PostAsync(resource, requestObject);

            if (result)
            {
                // Null check is not used, it's desired that news will exist in cache already
                _newsCache.FirstOrDefault(news => news.NewsId == newsId)
                    .UpdateFromDto(await LoadNewsByIdAsync(newsId));
                // It is also possible to add like by hands, but it is better to update whole news.

                // TODO: Update comment cache model too
            }

            return result;
        }

        public async Task<bool> UnlikeCommentAsync(string newsId, string commentId)
        {
            var resource = string.Format(LikeUnlikeCommentPath, newsId, commentId);

            var result = await _restClient.DeleteAsync(resource);

            if (result)
            {
                // Null check is not used, it's desired that news will exist in cache already
                _newsCache.FirstOrDefault(news => news.NewsId == newsId)
                    .UpdateFromDto(await LoadNewsByIdAsync(newsId));
                // It is also possible to add like by hands, but it is better to update whole news.

                // TODO: Update comment cache model too
            }

            return result;
        }

        public async Task<bool> AddCommentAsync(string author, string body, string newsId)
        {
            var resource = string.Format(NewsByIdPath, newsId) + "comments";
            var commentRequestDto = new CommentRequestDto
            {
                Push =
                {
                    authorId = author,
                    body = body,
                    date = DateTime.Now.Millisecond
                }
            };

            var result = await _restClient.PostAsync<bool>(resource, commentRequestDto);

            if (result)
            {
                // Null check is not used, it's desired that news will exist in cache already
                _newsCache.FirstOrDefault(news => news.NewsId == newsId)
                    .UpdateFromDto(await LoadNewsByIdAsync(newsId));
                // It is also possible to add like by hands, but it is better to update whole news.

                // TODO: Update comment cache model too
            }

            return result;
        }

        public async Task<NewsDto> LoadNewsByIdAsync(string newsId)
        {
            return await _restClient.GetAsync<NewsDto>(string.Format(NewsByIdPath, newsId));
        }

        public async Task<CommentsResponseDto> LoadListOfCommentsAsync(string newsId)
        {
            var resource = string.Format(NewsByIdPath, newsId) + "comments";

            return await _restClient.GetAsync<CommentsResponseDto>(resource);
        }

        private void SortNewsCache()
        {
            // OrderBy is dropped due to collection recreating
            _newsCache.Sort((n1, n2) => n2.Date.CompareTo(n1.Date));
        }
    }
}