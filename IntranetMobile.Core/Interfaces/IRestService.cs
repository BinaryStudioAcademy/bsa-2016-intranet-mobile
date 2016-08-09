namespace IntranetMobile.Core.Interfaces
{
    public interface IRestService
    {
        T Get<T>(string resource) where T : new();
        T Get<T>(string resource, object requestObject) where T : new();
        T Post<T>(string resource) where T : new();
        T Post<T>(string resource, object requestObject) where T : new();
    }
}