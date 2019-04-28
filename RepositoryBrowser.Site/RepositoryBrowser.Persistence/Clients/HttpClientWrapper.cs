using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RepositoryBrowser.Interfaces.Persistence.Clients;
using System.Net.Http;
using System.Threading.Tasks;

namespace RepositoryBrowser.Persistence.Clients
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings settings;

        public HttpClientWrapper(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        private async Task<T> GetResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content?.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return default(T);
        }

        public async Task<T> Get<T>(string route)
        {
            var response = await httpClient.GetAsync(route);
            return await GetResponse<T>(response);
        }
    }
}
