using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    public interface IDataBaseService
    {
        Task<bool> InsertItemAsync<T>(T item) where T : class, new();
        Task<bool> UpdateItemAsync<T>(T item) where T : class, new();
        Task<bool> UpdateOrInsertItemAsync<T>(T item) where T : class, new();
        Task<bool> DeleteItemAsync<T>(T item) where T : class, new();
        Task<IEnumerable<T>> GetAllItemsAsync<T>() where T : class, new();
        Task<T> GetFirstOrDefault<T>() where T : class, new();
    }
}