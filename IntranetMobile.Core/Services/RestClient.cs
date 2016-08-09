using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using IntranetMobile.Core.Interfaces;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Services
{
    public class RestClient : IRestClient
    {
        private const string UserAgent = "Fiddler";
        private const string BaseUrl = "http://team.binary-studio.com/";
        private readonly Uri baseUri;
        private readonly CookieContainer cookieContainer;
        private readonly HttpClient httpClient;

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

        public Task<bool> GetAsync(string resource)
        {
            return Execute(resource, null, HttpMethod.Get);
        }

        public Task<T> GetAsync<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, HttpMethod.Get);
        }

        public Task<bool> GetAsync(string resource, object requestObject)
        {
            return Execute(resource, requestObject, HttpMethod.Get);
        }

        public Task<T> PostAsync<T>(string resource) where T : new()
        {
            return Execute<T>(resource, null, HttpMethod.Post);
        }

        public Task<bool> PostAsync(string resource)
        {
            return Execute(resource, null, HttpMethod.Post);
        }

        public Task<T> PostAsync<T>(string resource, object requestObject) where T : new()
        {
            return Execute<T>(resource, requestObject, HttpMethod.Post);
        }

        public Task<bool> PostAsync(string resource, object requestObject)
        {
            return Execute(resource, requestObject, HttpMethod.Post);
        }

        private async Task<T> Execute<T>(string resource, object requestObject, HttpMethod method) where T : new()
        {
            var responseMessage = await GetResponse(resource, requestObject, method);
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

        private async Task<bool> Execute(string resource, object requestObject, HttpMethod method)
        {
            var responseMessage = await GetResponse(resource, requestObject, method);
            try
            {
                responseMessage.EnsureSuccessStatusCode();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private async Task<HttpResponseMessage> GetResponse(string resource, object requestObject, HttpMethod method)
        {
            HttpResponseMessage responseMessage = null;
            if (method == HttpMethod.Get)
            {
                IDictionary propertiesDictionary = null;
                if (requestObject != null)
                {
                    propertiesDictionary = requestObject.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(requestObject, null));
                }

                var url = BaseUrl
                    .AppendPathSegment(resource)
                    .SetQueryParams(propertiesDictionary);

                responseMessage = await httpClient.SendAsync(new HttpRequestMessage(method, url));
            }
            else
            {
                var content = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8);
                content.Headers.ContentType.MediaType = "application/json";
                responseMessage =
                    await
                        httpClient.SendAsync(new HttpRequestMessage(method, new Uri(baseUri, resource))
                        {
                            Content = content
                        });
            }

            return responseMessage;
        }
    }
}