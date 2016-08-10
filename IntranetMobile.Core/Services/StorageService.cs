using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using MvvmCross.Platform.Core;

namespace IntranetMobile.Core.Services
{
    public class StorageService : IStorageService
    {
        public StorageService(string path)
        {
            DbService = new DataBaseService(path);
        }

        public DataBaseService DbService { get; }

        public async Task<bool> AddItem<T>(T item) where T : new()
        {
            return await DbService.InsertItemAsync<T>(item);
        }

        public async Task<bool> UpdateItem<T>(T item) where T : new()
        {
            return await DbService.UpdateItemAsync<T>(item);
        }

        public async Task<bool> RemoveItem<T>(T item) where T : new()
        {
            return await DbService.DeleteItemAsync<T>(item);
        }

        public async Task<List<T>> GetAllItems<T>(T item) where T : new()
        {
            return (await DbService.GetAllItemsAsync<T>()).ToList();
        }
    }
}