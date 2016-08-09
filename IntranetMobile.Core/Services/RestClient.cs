using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Services
{
    public class RestClient : IRestClient
    {
        private const string UserAgent = "Fiddler";
        private const string BaseUrl = "http://team.binary-studio.com/";
        private readonly CookieContainer cookieContainer;
        private readonly HttpClient httpClient;
		private readonly Uri baseUri;

        public RestClient()
        {
			baseUri = new Uri(BaseUrl);
            cookieContainer = new CookieContainer();
            httpClient = new HttpClient(new HttpClientHandler {CookieContainer = cookieContainer});
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }

        public Task<T> GetAsync<T>(string resource) where T : new()
        {
            return Execute<T>(resource, null, HttpMethod.Get);
        }

        public Task<T> GetAsync<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, HttpMethod.Get);
        }

        public Task<T> PostAsync<T>(string resource) where T : new()
        {
            return Execute<T>(resource, null, HttpMethod.Post);
        }

        public Task<T> PostAsync<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, HttpMethod.Post);
        }

        private async Task<T> Execute<T>(string resource, object requestObject, HttpMethod method) where T : new()
        {
            var content = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8);
            content.Headers.ContentType.MediaType = "application/json";
            var responseMessage =
				await httpClient.SendAsync(new HttpRequestMessage(method, new Uri(baseUri, resource)) {Content = content});
			responseMessage.EnsureSuccessStatusCode();

            var responseString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}