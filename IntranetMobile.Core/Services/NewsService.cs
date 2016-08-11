using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Platform;

namespace IntranetMobile.Core.Services
{
	public class NewsService : INewsService
	{
		private const string type = "company";
		private const string published = "yes";
		private const string companyNewsPath = "api/news?";
		private const string weekNewsPath = "api/packs?";

		private RestClient restClient;
        private readonly RestClient restClient;

        public NewsService()
        {
            restClient = Mvx.Resolve<RestClient>();
        }

        public Task<List<CompNewsDto>> CompanyNews(int skip, int limit)
        {
            var compNewsReqParams = new CompNewsReqParams();

            compNewsReqParams.type = type;
            compNewsReqParams.limit = limit;
            compNewsReqParams.skip = skip;

            return restClient.GetAsync<List<CompNewsDto>>(companyNewsPath, compNewsReqParams);
        }

        public Task LikeComment(string newsId, string commentId)
        {
            throw new NotImplementedException();
        }

        public Task LikeNews(string id)
        {
            throw new NotImplementedException();
        }

        public Task UnlikeComment(string newsId, string CommentId)
        {
            throw new NotImplementedException();
        }

        public Task UnLikeNews(string id)
        {
            throw new NotImplementedException();
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

