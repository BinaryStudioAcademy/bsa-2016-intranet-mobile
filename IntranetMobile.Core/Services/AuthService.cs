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
		private const string resetPasswordPath = "auth/api/users/forgotPassword";

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

			var authDto = restClient.PostAsync<AuthDto>(loginPath, user, null);

			return authDto;
		}

		public Task Logout()
		{
			var logout = restClient.GetAsync<bool>(logoutPath);

			return logout;
		}

		public Task ResetPassword(string email)
		{
			var resetPassword = restClient.PostAsync<bool>(email, resetPasswordPath, null);
			return resetPassword;
		}
	}
}

