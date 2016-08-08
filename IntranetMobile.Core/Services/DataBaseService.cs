using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using SQLite;

namespace IntranetMobile.Core.Services
{
    public class DataBaseService : IDataBaseService
    {
        public DataBaseService(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public async Task CreateTableAsync<T>() where T : new()
        {
                var connection = new SQLiteAsyncConnection(Path);
                await connection.CreateTableAsync<T>();
        }

        public async Task<bool> InsertItemAsync<T>(object obj) where T : new()
        {
            try
            {
                await CreateTableAsync<T>();
                var db = new SQLiteAsyncConnection(Path);
                await db.InsertAsync(obj);
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync<T>(object obj) where T : new()
        {
            try
            {
                await CreateTableAsync<T>();
                var db = new SQLiteAsyncConnection(Path);
                await db.UpdateAsync(obj);
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync<T>(object obj) where T : new()
        {
            try
            {
                await CreateTableAsync<T>();
                var db = new SQLiteAsyncConnection(Path);
                await db.DeleteAsync(obj);
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync<T>() where T : new()
        {
            try
            {
                await CreateTableAsync<T>();
                var db = new SQLiteAsyncConnection(Path);
                return await db.Table<T>().ToListAsync();
            }
            catch (SQLiteException)
            {
                return null;
            }
        }
    }
}
