using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Core.Services
{
    public class StorageService : IStorageService
    {
        private IDataBaseService dataBaseService;

        public StorageService(IDataBaseService dataBaseService)
        {
			this.dataBaseService = dataBaseService;
        }

        public Task<bool> AddItem<T>(T item) where T : class, new()
        {
            return dataBaseService.InsertItemAsync(item);
        }

        public Task<bool> UpdateItem<T>(T item) where T : class, new()
        {
            return dataBaseService.UpdateItemAsync(item);
        }

		public Task<bool> AddOrUpdateItem<T>(T item) where T : class, new()
		{
			return dataBaseService.UpdateOrInsertItemAsync(item);
		}

        public Task<bool> RemoveItem<T>(T item) where T : class, new()
        {
            return dataBaseService.DeleteItemAsync(item);
        }

		public Task<IEnumerable<T>> GetAllItems<T>() where T : class, new()
        {
            return dataBaseService.GetAllItemsAsync<T>();
        }

		public Task<T> GetFirstOrDefault<T>() where T : class, new()
		{
			return dataBaseService.GetFirstOrDefault<T>();
		}
    }
}