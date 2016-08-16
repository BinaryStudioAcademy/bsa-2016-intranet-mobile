using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Core.Services
{
    public class StorageService : IStorageService
    {
        private readonly IDataBaseService _dataBaseService;

        public StorageService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public Task<bool> AddItem<T>(T item) where T : class, new()
        {
            return _dataBaseService.InsertItemAsync(item);
        }

        public Task<bool> UpdateItem<T>(T item) where T : class, new()
        {
            return _dataBaseService.UpdateItemAsync(item);
        }

        public Task<bool> AddOrUpdateItem<T>(T item) where T : class, new()
        {
            return _dataBaseService.UpdateOrInsertItemAsync(item);
        }

        public Task<bool> RemoveItem<T>(T item) where T : class, new()
        {
            return _dataBaseService.DeleteItemAsync(item);
        }

        public Task<IEnumerable<T>> GetAllItems<T>() where T : class, new()
        {
            return _dataBaseService.GetAllItemsAsync<T>();
        }

        public Task<T> GetFirstOrDefault<T>() where T : class, new()
        {
            return _dataBaseService.GetFirstOrDefault<T>();
        }
    }
}