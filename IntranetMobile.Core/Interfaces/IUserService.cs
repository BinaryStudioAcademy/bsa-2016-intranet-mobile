using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
    }
}