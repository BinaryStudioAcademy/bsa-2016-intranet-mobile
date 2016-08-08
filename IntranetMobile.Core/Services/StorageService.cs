using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;
using SQLite;

namespace IntranetMobile.Core.Services
{
    public class StorageService : IStorageService
    {
        public DataBaseService DbService { get; }

        public StorageService(string path)
        {
            DbService = new DataBaseService(path);
        }

        public async Task<bool> RememberUser(User user)
        {
            return await DbService.InsertItemAsync<User>(user);
        }

        public async Task<bool> ForgetUser(User user)
        {
            return await DbService.DeleteItemAsync<User>(user);
        }
    }
}
