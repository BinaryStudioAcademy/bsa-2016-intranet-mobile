using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net.Async;

namespace IntranetMobile.Core.Services
{
    public class DataBaseService : IDataBaseService
    {
        private const string FileName = "/db.db";
        private readonly ILogger logger;
        private readonly IMvxSqliteConnectionFactory sqliteConnectionFactory;
        private SQLiteAsyncConnection connection;
        private string fileDir;

        public DataBaseService(IMvxSqliteConnectionFactory sqliteConnectionFactory)
        {
            //Path = path + FileName;
            this.sqliteConnectionFactory = sqliteConnectionFactory;
            logger = Mvx.Resolve<ILogger>();
        }

        public string Path { get; set; }

        public string FileDir
        {
            get { return fileDir; }
            set
            {
                fileDir = value;
                Path = fileDir + FileName;
            }
        }

        public void Init()
        {
            connection = sqliteConnectionFactory.GetAsyncConnection(Path);
        }

        public async Task<bool> InsertItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await CreateTableAndGetConnectionAsync<T>();
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
                await CreateTableAndGetConnectionAsync<T>();
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

        public async Task<bool> DeleteItemAsync<T>(T item) where T : class, new()
        {
            try
            {
                await CreateTableAndGetConnectionAsync<T>();
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
                await CreateTableAndGetConnectionAsync<T>();
                return await connection.Table<T>().ToListAsync();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return null;
            }
        }


        private async Task CreateTableAndGetConnectionAsync<T>() where T : class, new()
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