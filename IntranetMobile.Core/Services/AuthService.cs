using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;
using MvvmCross.Platform;

namespace IntranetMobile.Core.Services
{
    public class AuthService : IAuthService
    {
        private const string loginPath = "auth/api/login";
        private const string logoutPath = "auth/logout";
        private const string resetPasswordPath = "auth/api/users/forgotPassword";
        private const string resetPasswordContentType = "application/x-www-form-urlencoded";

        private readonly RestClient restClient;

        public AuthService()
        {
            restClient = Mvx.Resolve<RestClient>();
        }

        public Task<AuthDto> Login(string email, string paswword)
        {
            var user = new UserCredentialsDto
            {
                email = email,
                password = paswword
            };

            var authDto = restClient.PostAsync<AuthDto>(loginPath, user);

            return authDto;
        }

        public Task<bool> Logout()
        {
            return restClient.GetAsync(logoutPath);
        }

        public Task<bool> ResetPassword(string email)
        {
            return restClient.PostAsync(resetPasswordPath, new {email}, resetPasswordContentType);
        }
    }
}