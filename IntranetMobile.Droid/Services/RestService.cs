using IntranetMobile.Core.Interfaces;
using RestSharp;

namespace IntranetMobile.Droid.Services
{
    public class RestService : IRestService
    {
        private const string UserAgent = "Fiddler";
        private const string BaseUrl = "http://team.binary-studio.com/";

        public T Get<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, Method.GET);
        }

        public T Post<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, Method.POST);
        }

        private T Execute<T>(string resource, object requestObject, Method method) where T : new()
        {
            var client = new RestClient(BaseUrl + resource) {UserAgent = UserAgent};
            var request = new RestRequest(method);
            request.AddObject(requestObject);
            return client.Execute<T>(request).Data;
        }
    }
}