using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string resource) where T : new();
        Task<T> GetAsync<T>(string resource, object requestObject) where T : new();
        Task<T> PostAsync<T>(string resource) where T : new();
        Task<T> PostAsync<T>(string resource, object requestObject) where T : new();
    }
}