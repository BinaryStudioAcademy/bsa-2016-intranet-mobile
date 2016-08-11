using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;
using MvvmCross.Platform;

namespace IntranetMobile.Core.Services
{
	public class NewsService : INewsService
	{
		private const string type = "company";
		private const string published = "yes";
		private const string weekNewsPath = "api/packs?";
		private const string likeUnlikeCommentPath = "api/news/{0}/comments/{1}/likes";
		private const string likeUnlikeNews = "api/news/{0}/likes";

        private readonly RestClient restClient;

        public NewsService()
        {
            restClient = Mvx.Resolve<RestClient>();
        }

        public Task<List<CompNewsDto>> CompanyNews(int skip, int limit)
        {
			var resource = "api/news?";

            var compNewsReqParams = new CompNewsReqParams();

            compNewsReqParams.type = type;
            compNewsReqParams.limit = limit;
            compNewsReqParams.skip = skip;

			return restClient.GetAsync<List<CompNewsDto>>(resource, compNewsReqParams);
        }

		public Task<bool> LikeComment(string newsId, string commentId)
        {
			var resource = String.Format(likeUnlikeCommentPath, newsId, commentId);

			var requestObject = new CompNewsLikeCommentDto();

			requestObject.newsId = newsId;
			requestObject.commentId = commentId;

			return restClient.PostAsync(resource, requestObject);
        }

        public Task<bool> LikeNews(string newsId)
        {
			var resource = String.Format(likeUnlikeNews, newsId);

			var requestObject = new CompNewsLikeNewsDto();

			requestObject.id = newsId;

			return restClient.PostAsync(resource, requestObject);
        }

        public Task<bool> UnlikeComment(string newsId, string CommentId)
        {
			var resource = String.Format(likeUnlikeCommentPath, newsId, CommentId);

			return restClient.DeleteAsync(resource);

        }

        public Task<bool> UnLikeNews(string id)
        {
			var resource = String.Format(likeUnlikeNews, id);

			return restClient.DeleteAsync(resource);
        }

		public Task<List<WeekNewsDto>> Weeklies(int skip, int limit)
		{
			var weekNewsReqParams = new WeekNewsReqParams();

			weekNewsReqParams.skip = skip;
			weekNewsReqParams.limit = limit;
			weekNewsReqParams.published = published;

			return restClient.GetAsync<List<WeekNewsDto>>(weekNewsPath, weekNewsReqParams);
		}
	}
}

