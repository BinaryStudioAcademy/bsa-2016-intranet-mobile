using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;

namespace IntranetMobile.Core.Interfaces
{
    public interface IStorageService
    {
        Task<bool> AddItem<T>(T item) where T : new();
        Task<bool> RemoveItem<T>(T item) where T : new();

    }
}
