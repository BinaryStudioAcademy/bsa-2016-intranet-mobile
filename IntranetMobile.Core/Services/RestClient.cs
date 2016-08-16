using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Services
{
    public class RestClient
    {
        private const string UserAgent = "Fiddler";
        private const string ContentType = "application/json";
        private const string BaseUrl = "http://team.binary-studio.com/";
        private readonly Uri _baseUri;
        private readonly CookieContainer _cookieContainer;
        private readonly HttpClient _httpClient;

        public RestClient()
        {
            _baseUri = new Uri(BaseUrl);
            _cookieContainer = new CookieContainer();
            _httpClient = new HttpClient(new HttpClientHandler {CookieContainer = _cookieContainer});
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }

        public Task<T> GetAsync<T>(string resource) where T : new()
        {
            return Execute<T>(resource, null, HttpMethod.Get);
        }

        public Task<bool> GetAsync(string resource)
        {
            return Execute(resource, null, HttpMethod.Get);
        }

        public Task<bool> DeleteAsync(string resource)
        {
            return Execute(resource, null, HttpMethod.Delete);
        }

        public Task<T> GetAsync<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, HttpMethod.Get);
        }

        public Task<T> PostAsync<T>(string resource, string contentType = ContentType) where T : new()
        {
            return Execute<T>(resource, null, HttpMethod.Post, contentType);
        }

        public Task<T> PostAsync<T>(string resource, object requestObject, string contentType = ContentType)
            where T : new()
        {
            return Execute<T>(resource, requestObject, HttpMethod.Post, contentType);
        }

        public Task<bool> PostAsync(string resource, object requestObject, string contentType = ContentType)
        {
            return Execute(resource, requestObject, HttpMethod.Post, contentType);
        }

        private async Task<T> Execute<T>(string resource, object requestObject, HttpMethod method,
            string contentType = ContentType) where T : new()
        {
            var responseMessage = await GetResponse(resource, requestObject, method, contentType);
            try
            {
                responseMessage.EnsureSuccessStatusCode();
                var responseString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch
            {
                return default(T);
            }
        }

        private async Task<bool> Execute(string resource, object requestObject, HttpMethod method,
            string contentType = ContentType)
        {
            try
            {
                var responseMessage = await GetResponse(resource, requestObject, method, contentType);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private async Task<HttpResponseMessage> GetResponse(string resource, object requestObject, HttpMethod method,
            string contentType)
        {
            HttpResponseMessage responseMessage;
            if (method == HttpMethod.Get || method == HttpMethod.Delete)
            {
                var uriBuilder = new UriBuilder(new Uri(_baseUri, resource));

                if (requestObject != null)
                {
                    var propertiesDictionary = requestObject.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(prop => !prop.GetCustomAttributes(typeof(JsonIgnoreAttribute), false).Any())
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(requestObject).ToString())
                        .ToList();

                    var param = new FormUrlEncodedContent(propertiesDictionary);
                    uriBuilder.Query = await param.ReadAsStringAsync();
                }

                responseMessage = await _httpClient.SendAsync(new HttpRequestMessage(method, uriBuilder.ToString()));
            }
            else
            {
                var content = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8);
                content.Headers.ContentType.MediaType = contentType;
                responseMessage =
                    await
                        _httpClient.SendAsync(new HttpRequestMessage(method, new Uri(_baseUri, resource))
                        {
                            Content = content
                        });
            }

            return responseMessage;
        }
    }
}