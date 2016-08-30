using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface IUserService
    {
        UserInfo CurrentUser { get; }

        Task<UserInfo> GetCurrentUserAsync();

        Task<List<UserInfo>> GetAllUsers();

        Task<UserInfo> GetUserInfoById(string id);

        Task<User> GetUserByServerId(string id);

        Task<Position> GetPositionById(string id);

        Task<List<Position>> GetAllPositions();

        Task<Technology> GetTechnologyById(string id);

        Task<List<Technology>> GetAllTechnologies();

        Task<UserCvs> GetUserCvsByServerId(string id);
    }
}