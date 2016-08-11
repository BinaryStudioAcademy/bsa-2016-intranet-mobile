using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    public interface IStorageService
    {
        Task<bool> AddItem<T>(T item) where T : class, new();

        /// <summary>
        ///     Updates item if exists or add item if it doesn't exist.
        /// </summary>
        Task<bool> AddOrUpdateItem<T>(T item) where T : class, new();

        Task<bool> RemoveItem<T>(T item) where T : class, new();

        Task<bool> UpdateItem<T>(T item) where T : class, new();

        Task<IEnumerable<T>> GetAllItems<T>() where T : class, new();

        Task<T> GetFirstOrDefault<T>() where T : class, new();
    }
}