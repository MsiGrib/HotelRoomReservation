using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace Web
{
    public class UniversalApiManager
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UniversalApiManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET-запрос
        public async Task<T> GetAsync<T>(string clientName, string requestUri)
        {
            var client = _httpClientFactory.CreateClient(clientName);

            return await client.GetFromJsonAsync<T>(requestUri);
        }

        // POST-запрос
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string clientName, string requestUri, TRequest data)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                var response = await client.PostAsJsonAsync(requestUri, data);

                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (Exception ex)
            {
                var qwe = ex;
                throw;
            }

        }

        // PUT-запрос
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string clientName, string requestUri, TRequest data)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var response = await client.PutAsJsonAsync(requestUri, data);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        // DELETE-запрос
        public async Task DeleteAsync(string clientName, string requestUri)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var response = await client.DeleteAsync(requestUri);
        }

        // PATCH-запрос
        public async Task<TResponse> PatchAsync<TRequest, TResponse>(string clientName, string requestUri, TRequest data)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };
            var response = await client.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
    }
}
