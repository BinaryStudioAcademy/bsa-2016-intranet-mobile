using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface IUserService
    {
        User CurrentUser { get; }

        Task<User> GetCurrentUserAsync();

        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(string id);
    }
}