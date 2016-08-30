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
        private const string AllUsersInfoPath = "/profile/api/users/filter";
        private const string AllUsersPath = "profile/api/users";
        private const string CurrentUserPath = "/api/me";
        private const string PositionsPath = "profile/api/positions";
        private const string TechnologiesPath = "profile/api/technologies";
        private const string CvsPath = "profile/api/cvs";
        private readonly List<Position> _cachedPositions = new List<Position>();
        private readonly List<Technology> _cachedTechnologies = new List<Technology>();
        private readonly List<UserInfo> _cachedUsers = new List<UserInfo>();
        private readonly RestClient _restClient;

        private readonly SemaphoreSlim _semaphoreAllUser;

        public UserService(RestClient client)
        {
            _semaphoreAllUser = new SemaphoreSlim(1);
            _restClient = client;
        }

        public async Task<UserInfo> GetCurrentUserAsync()
        {
            if (CurrentUser == null)
            {
                await GetAllUsers();
            }
            return CurrentUser;
        }

        public async Task<List<UserInfo>> GetAllUsers()
        {
            await _semaphoreAllUser.WaitAsync().ConfigureAwait(false);

            if (_cachedUsers.Count > 0)
            {
                _semaphoreAllUser.Release();
                return new List<UserInfo>(_cachedUsers);
            }

            var usersInfoTask = _restClient.GetAsync<List<UserInfoDto>>(AllUsersInfoPath);
            var usersTask = _restClient.GetAsync<List<UserDto>>(AllUsersPath);
            var currentUserTask = _restClient.GetAsync<MyUser>(CurrentUserPath);
            await Task.WhenAll(usersInfoTask, usersTask, currentUserTask);

            var userInfoDtos = await usersInfoTask;
            var userDtos = await usersTask;
            var currentUserDto = await currentUserTask;

            foreach (var dto in userDtos)
            {
                var item = userInfoDtos.FirstOrDefault(i => i.Id == dto.Id);
                if (item != null)
                {
                    item.ServerId = dto.ServerUserId;
                }
            }

            _cachedUsers.AddRange(userInfoDtos.Select(userDto => new UserInfo().UpdateFromDto(userDto)));

            CurrentUser = _cachedUsers.FirstOrDefault(u => u.ServerId == currentUserDto.Id);
            CurrentUser.Email = currentUserDto?.Email;

            _semaphoreAllUser.Release();

            // Don't need to be awaited because at this moment we don't use results of those tasks
            GetAllPositions();
            GetAllTechnologies();

            // Prevent user-defined code from cache modifying
            return new List<UserInfo>(_cachedUsers);
        }

        public async Task<UserInfo> GetUserInfoById(string id)
        {
            if (_cachedUsers.Count == 0)
            {
                await GetAllUsers();
            }
            return _cachedUsers.FirstOrDefault(u => u.ServerId.Equals(id));
        }

        public async Task<User> GetUserByServerId(string id)
        {
            var path = $"{AllUsersPath}/{id}";
            var userDto = await _restClient.GetAsync<UserDto>(path);
            return new User().UpdateFromDto(userDto);
        }

        public async Task<UserCvs> GetUserCvsByServerId(string id)
        {
            var path = $"{CvsPath}/{id}";
            var userCvsDto = await _restClient.GetAsync<UserCvsDto>(path);
            return new UserCvs().UpdateFromDto(userCvsDto);
        }

        public UserInfo CurrentUser { get; private set; }

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
            var positionsDto = await _restClient.GetAsync<List<PositionDto>>(PositionsPath).ConfigureAwait(false);

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

        public async Task<Technology> GetTechnologyById(string id)
        {
            // For now every time we try to get position we will already have all the positions.
            if (_cachedTechnologies.Count == 0)
            {
                await GetAllTechnologies();
            }
            return _cachedTechnologies.FirstOrDefault(technology => technology.Id == id);
        }

        public async Task<List<Technology>> GetAllTechnologies()
        {
            var technologiesDto = await _restClient.GetAsync<List<TechnologyRootDto>>(TechnologiesPath).ConfigureAwait(false);

            if (_cachedTechnologies.Count > 0)
            {
                return new List<Technology>(_cachedTechnologies);
            }

            // TODO: For possible future runtime updates it is necessary to update existing users
            // TODO: And create UpdateFromDto method for User

            _cachedTechnologies.AddRange(technologiesDto.Select(technologyDto => new Technology().UpdateFromDto(technologyDto)));

            // Prevent user-defined code from cache modifying
            return new List<Technology>(_cachedTechnologies);
        }
    }
}