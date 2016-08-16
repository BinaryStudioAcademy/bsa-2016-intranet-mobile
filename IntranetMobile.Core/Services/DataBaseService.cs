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
        private readonly SQLiteAsyncConnection _connection;
        private readonly ILogger _logger;

        public DataBaseService(string fileDir, IMvxSqliteConnectionFactory sqliteConnectionFactory, ILogger logger)
        {
            var path = fileDir + FileName;
            _logger = logger;

            _connection = sqliteConnectionFactory.GetAsyncConnection(path);
        }

        public async Task<bool> InsertItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await CreateTableAsync<T>();
                await _connection.InsertAsync(item);
                _logger.Info("Item was inserted");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await CreateTableAsync<T>();
                await _connection.UpdateAsync(item);
                _logger.Info("Item was updated");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOrInsertItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await CreateTableAsync<T>();
                await _connection.InsertOrReplaceAsync(item);
                _logger.Info("Item was inserted or updated");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await CreateTableAsync<T>();
                await _connection.DeleteAsync(item);
                _logger.Info("Item was deleted");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync<T>() where T : class, new()
        {
            try
            {
                await CreateTableAsync<T>();
                return await _connection.Table<T>().ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return null;
            }
        }

        public async Task<T> GetFirstOrDefault<T>() where T : class, new()
        {
            try
            {
                await CreateTableAsync<T>();
                return await _connection.Table<T>().FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return null;
            }
        }

        private async Task CreateTableAsync<T>() where T : class, new()
        {
            try
            {
                await _connection.CreateTableAsync<T>();
                _logger.Info("Db was created");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }
    }
}