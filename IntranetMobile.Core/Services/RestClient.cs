using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Services
{
    public class RestClient
    {
        public T Get<T>(object dto)
        {
            string resultString;

            //try
            //{
            //    using (var client = new HttpClient())
            //    using (var response = await client.GetAsync(url))
            //    {
            //        var bytes = await response.Content.ReadAsByteArrayAsync();
            //        resultString = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Logger.Error(exception);
            //    await AvailabilityCheckService.CheckAvailableAsync(BaseUrl);
            //    return null;
            //}

            //try
            //{
            //    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<OpenWeatherBase.RootObject>(resultString));
            //}
            //catch (Exception exception)
            //{
            //    Logger.Error(exception);
            //    return null;
            //}

            return default(T);
        }
    }
}
