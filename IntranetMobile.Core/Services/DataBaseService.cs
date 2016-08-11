using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net.Async;

namespace IntranetMobile.Core.Services
{
    public class DataBaseService : IDataBaseService
    {
        private const string FileName = "/db.db";
        private readonly ILogger logger;
        private readonly SQLiteAsyncConnection connection;

        public DataBaseService(string fileDir, IMvxSqliteConnectionFactory sqliteConnectionFactory, ILogger logger)
        {
            path = fileDir + FileName;
            this.logger = logger;

            connection = sqliteConnectionFactory.GetAsyncConnection(path);
        }

        private string path { get; }

        public async Task<bool> InsertItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await createTableAsync<T>();
                await connection.InsertAsync(item);
                logger.Info("Item was inserted");
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await createTableAsync<T>();
                await connection.UpdateAsync(item);
                logger.Info("Item was updated");
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOrInsertItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await createTableAsync<T>();
                await connection.InsertOrReplaceAsync(item);
                logger.Info("Item was inserted or updated");
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await createTableAsync<T>();
                await connection.DeleteAsync(item);
                logger.Info("Item was deleted");
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync<T>() where T : class, new()
        {
            try
            {
                await createTableAsync<T>();
                return await connection.Table<T>().ToListAsync();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return null;
            }
        }

        public async Task<T> GetFirstOrDefault<T>() where T : class, new()
        {
            try
            {
                await createTableAsync<T>();
                return await connection.Table<T>().FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return null;
            }
        }


        private async Task createTableAsync<T>() where T : class, new()
        {
            try
            {
                await connection.CreateTableAsync<T>();
                logger.Info("Db was created");
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
    }
}