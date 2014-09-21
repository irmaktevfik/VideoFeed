using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VideoApp.Repository
{
    public class BaseRepository
    {
        private HttpClient httpClient = new HttpClient();
        //Change this line if you want to reference to another URL for VideoApp.Services
        private const string BaseUri = "http://videofeeds.azurewebsites.net/api/"; //this needs to be changed to the URI of your Service URL

        public static string FullUrl(UriString parameters, string MethodName, string ControllerName)
        {
            List<string> returnParams = new List<string>();
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> param in parameters._Params)
                {
                    returnParams.Add(String.Format("{0}={1}", param.Key, param.Value));
                }
                var data = "?" + String.Join("&", returnParams.ToArray());
                return BaseUri + ControllerName + "/" + MethodName + data;
            }
            else
                return BaseUri + ControllerName + "/" + MethodName;

        }

        public BaseRepository()
        {
            HttpClientHandler handle = new HttpClientHandler();
            httpClient = new HttpClient(handle);
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 Safari/537.36");
        }

        #region GetAsync
        protected async Task<T> GetAsync<T>([CallerMemberName]string MethodName = null, string ControllerName = null)
        {
            return await GetAsync<T>(null, MethodName, ControllerName);
        }

        protected async Task<T> GetAsync<T>(UriString parameters, [CallerMemberName]string MethodName = null, string ControllerName = null)
        {
            var result = await this.httpClient.GetAsync(FullUrl(parameters, MethodName, ControllerName));
            if (result.IsSuccessStatusCode)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        protected async Task<HttpResponseMessage> GetAsync([CallerMemberName]string MethodName = null, string ControllerName = null)
        {
            return await GetAsync(null, MethodName, ControllerName);

        }

        protected async Task<HttpResponseMessage> GetAsync(UriString parameters, [CallerMemberName]string MethodName = null, string ControllerName = null)
        {
            var fullUrl = BaseUri + ControllerName + MethodName + parameters.ToString();
            var result = await this.httpClient.GetAsync(FullUrl(parameters, MethodName, ControllerName));
            return result;

        }

        #endregion
    }
    public class UriString
    {
        public Dictionary<string, string> _Params = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            _Params.Add(key, value);
        }
    }
}
