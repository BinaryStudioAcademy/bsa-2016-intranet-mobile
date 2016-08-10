using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    public interface IStorageService
    {
        IDataBaseService DataBaseService { get; set; }
        Task<bool> AddItem<T>(T item) where T : class, new();
        Task<bool> RemoveItem<T>(T item) where T : class, new();
        Task<bool> UpdateItem<T>(T item) where T : class, new();
        Task<List<T>> GetAllItems<T>() where T : class, new();
    }
}