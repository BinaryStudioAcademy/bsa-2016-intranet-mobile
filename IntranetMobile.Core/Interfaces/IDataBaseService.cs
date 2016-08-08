using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    public interface IDataBaseService
    {
        Task CreateTableAsync<T>() where T : new();
        Task<bool> InsertItemAsync<T>(object obj) where T : new();
        Task<bool> UpdateItemAsync<T>(object obj) where T : new();
        Task<bool> DeleteItemAsync<T>(object obj) where T : new();
        Task<IEnumerable<T>> GetAllItemsAsync<T>() where T : new();
    }
}
