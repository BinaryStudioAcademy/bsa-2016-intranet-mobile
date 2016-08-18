using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Services
{
    public class UserService : IUserService
    {
        private const string ApiUrl = "profile/api/users";
        private readonly RestClient _restClient;

        public UserService(RestClient client)
        {
            _restClient = client;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _restClient.GetAsync<List<UserDto>>(ApiUrl);
        }
    }
}