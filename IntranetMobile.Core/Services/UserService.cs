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
        private List<UserDto> _cachedUsers;

        public UserService(RestClient client)
        {
            _restClient = client;
        }

        // TODO: GET BY ID METHOD ADD!!!!!!!!!!!!!!!!!
        public async Task<List<UserDto>> GetAllUsers()
        {
            return _cachedUsers ?? (_cachedUsers = await _restClient.GetAsync<List<UserDto>>(ApiUrl));
        }
    }
}