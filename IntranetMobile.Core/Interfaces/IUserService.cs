using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(string id);
    }
}