using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface IStorageService
    {
        Task<bool> RememberUser(User user);
        Task<bool> ForgetUser(User user);
    }
}
