using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;
using MvvmCross.Platform;

namespace IntranetMobile.Core.Services
{
    public class AuthService : IAuthService
    {
        private const string LoginPath = "auth/api/login";
        private const string LogoutPath = "auth/logout";
        private const string ResetPasswordPath = "auth/api/users/forgotPassword";
        private const string ResetPasswordContentType = "application/x-www-form-urlencoded";

        private readonly RestClient _restClient;

        public AuthService()
        {
            _restClient = Mvx.Resolve<RestClient>();
        }

        public async Task<AuthDto> Login(string email, string paswword)
        {
            var user = new UserCredentialsDto
            {
                email = email,
                password = paswword
            };

            var authDto = await _restClient.PostAsync<AuthDto>(LoginPath, user);

            if (authDto.success)
            {
                ServiceBus.UserService.GetAllUsers();
                ServiceBus.UserService.ReviewerLoginAsync();
            }

            return authDto;
        }

        public Task<bool> Logout()
        {
            return _restClient.GetAsync(LogoutPath);
        }

        public Task<bool> ResetPassword(string email)
        {
            return _restClient.PostAsync(ResetPasswordPath, new {email}, ResetPasswordContentType);
        }
    }
}