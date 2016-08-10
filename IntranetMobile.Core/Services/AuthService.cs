using System;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;
using MvvmCross.Platform;

namespace IntranetMobile.Core
{
	public class AuthService : IAuthService
	{
		private const string loginPath = "auth/api/login";
		private const string logoutPath = "auth/logout";

		private IRestClient restClient;

		public AuthService()
		{
			restClient = Mvx.Resolve<IRestClient>();
		}

		public Task<AuthDto> Login(string email, string paswword)
		{
			var user = new User();

			user.Email = email;
			user.Password = paswword;

			var authDto = restClient.PostAsync<AuthDto>(loginPath, user);

			return authDto;
		}

		public Task Logout()
		{
			throw new NotImplementedException();
		}

		public Task ResetPassword(string email)
		{
			throw new NotImplementedException();
		}
	}
}

