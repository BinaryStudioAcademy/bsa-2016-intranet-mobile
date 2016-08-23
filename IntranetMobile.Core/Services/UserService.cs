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
        private readonly List<Position> _cachedPositions = new List<Position>();
        private readonly List<User> _cachedUsers = new List<User>();
        private readonly RestClient _restClient;

        private readonly SemaphoreSlim _semaphoreAllUser;

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

            if (_cachedUsers.Count > 0)
            {
                _semaphoreAllUser.Release();
                return new List<User>(_cachedUsers);
            }

            var userDtos = await _restClient.GetAsync<List<UserDto>>(AllUsersPath).ConfigureAwait(false);
            var currentUserDto = await _restClient.GetAsync<MyUser>(CurrentUserPath).ConfigureAwait(false);
            await GetAllPositions();

            // TODO: For possible future runtime updates it is necessary to update existing users
            // TODO: And create UpdateFromDto method for User

            _cachedUsers.AddRange(userDtos.Select(u => new User
            {
                Email = u.Email,
                UserId = u.ServerUserId,
                FirstName = u.Name,
                LastName = u.Surname,
                Birthday = DateTime.Parse(u.Birthday),
                AvatarUri = u.Avatar.UrlAva,
                PositionId = u.Position,
                Country = u.Country,
                City = u.City,
                Gender = u.Gender,
                HireDate = DateTime.Parse(u.WorkDate)
            }));

            CurrentUser = _cachedUsers.FirstOrDefault(u => u.UserId == currentUserDto.Id);

            _semaphoreAllUser.Release();

            // Prevent user-defined code from cache modifying
            return new List<User>(_cachedUsers);
        }

        public async Task<User> GetUserById(string id)
        {
            if (_cachedUsers.Count == 0)
            {
                await GetAllUsers();
            }
            return _cachedUsers.FirstOrDefault(u => u.UserId.Equals(id));
        }

        public User CurrentUser { get; private set; }

        public async Task<Position> GetPositionById(string id)
        {
            // For now every time we try to get position we will already have all the positions.
            if (_cachedPositions.Count == 0)
            {
                await GetAllPositions();
            }
            return _cachedPositions.FirstOrDefault(pos => pos.Id == id);
        }

        public async Task<List<Position>> GetAllPositions()
        {
            var positionsDto = (await _restClient.GetAsync<List<PositionDto>>(PositionPath).ConfigureAwait(false))
                .Where(pos => !pos.IsDeleted);

            if (_cachedPositions.Count > 0)
            {
                return new List<Position>(_cachedPositions);
            }

            // TODO: For possible future runtime updates it is necessary to update existing users
            // TODO: And create UpdateFromDto method for User

            _cachedPositions.AddRange(positionsDto.Select(pos => new Position
            {
                Name = pos.Name,
                Id = pos.Id
            }));

            // Prevent user-defined code from cache modifying
            return new List<Position>(_cachedPositions);
        }
    }
}