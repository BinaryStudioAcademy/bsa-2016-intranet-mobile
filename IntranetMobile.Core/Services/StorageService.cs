using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;

namespace IntranetMobile.Core.Services
{
    public class StorageService : IStorageService
    {
        private readonly string path;
        private IDataBaseService dataBaseService;

        public StorageService(string path)
        {
            this.path = path;
        }

        public IDataBaseService DataBaseService
        {
            get { return dataBaseService; }
            set
            {
                dataBaseService = value;
                DataBaseService.FileDir = path;
                DataBaseService.Init();
            }
        }


        public async Task<bool> AddItem<T>(T item) where T : class, new()
        {
            return await DataBaseService.InsertItemAsync(item);
        }

        public async Task<bool> UpdateItem<T>(T item) where T : class, new()
        {
            return await DataBaseService.UpdateItemAsync(item);
        }

        public async Task<bool> RemoveItem<T>(T item) where T : class, new()
        {
            return await DataBaseService.DeleteItemAsync(item);
        }

        public async Task<List<T>> GetAllItems<T>() where T : class, new()
        {
            return (await DataBaseService.GetAllItemsAsync<T>()).ToList();
        }
    }
}