using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Services
{
    public class UserService : IUserService
    {
        private const string AllUsersPath = "profile/api/users";
        private const string CurrentUserPath = "/api/me";
        private const string PositionPath = "profile/api/positions";
        private readonly RestClient _restClient;

        private readonly SemaphoreSlim _semaphoreAllUser;
        private List<Position> _cachedPositions;
        private List<User> _cachedUsers;

        public UserService(RestClient client)
        {
            _semaphoreAllUser = new SemaphoreSlim(1);
            _restClient = client;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            if (CurrentUser == null)
            {
                await GetAllUsers();
            }
            return CurrentUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            await _semaphoreAllUser.WaitAsync().ConfigureAwait(false);

            if (_cachedUsers != null && _cachedUsers.Count > 0)
            {
                _semaphoreAllUser.Release();
                return _cachedUsers;
            }

            var users = await _restClient.GetAsync<List<UserDto>>(AllUsersPath).ConfigureAwait(false);
            var currentUserDto = await _restClient.GetAsync<MyUser>(CurrentUserPath).ConfigureAwait(false);
            await GetAllPositions();

            _cachedUsers = users.Select(u => new User
            {
                Email = u.Email,
                UserId = u.ServerUserId,
                FirstName = u.Name,
                LastName = u.Surname,
                Birthday = DateTime.Parse(u.Birthday),
                AvatarUri = u.Avatar.UrlAva,
                Position = u.Position,
                Country = u.Country,
                City = u.City,
                Gender = u.Gender,
                HireDate = DateTime.Parse(u.WorkDate)
            }).ToList();

            CurrentUser = _cachedUsers.FirstOrDefault(u => u.UserId == currentUserDto.Id);

            _semaphoreAllUser.Release();
            return _cachedUsers;
        }

        public Position GetPositionById(string id)
        {
            //if(_cachedPositions !=null && _cachedPositions.Count > 0)
            //{
            //    return _cachedPositions.FirstOrDefault(pos => pos.Id == id);
            //}
            //_cachedPositions = await GetAllPositions();
            return _cachedPositions.FirstOrDefault(pos => pos.Id == id);
        }

        private async Task<List<Position>> GetAllPositions()
        {
            var positionsDto = (await _restClient.GetAsync<List<PositionDto>>(PositionPath).ConfigureAwait(false))
                                 .Where(pos=> !pos.IsDeleted);
            _cachedPositions = positionsDto.Select(pos => new Position()
            {
                Name = pos.Name,
                Id = pos.Id
            }).ToList();
                
            return _cachedPositions;
        }

        public async Task<User> GetUserById(string id)
        {
            User result;

            if (_cachedUsers != null && _cachedUsers.Count > 0)
            {
                result = _cachedUsers.FirstOrDefault(u => u.UserId.Equals(id));
                return result;
            }

            _cachedUsers = await GetAllUsers();
            result = _cachedUsers.FirstOrDefault(u => u.UserId.Equals(id));

            return result;
        }

        public User CurrentUser { get; private set; }
    }
}