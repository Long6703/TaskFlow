using Microsoft.AspNetCore.Mvc;

namespace Client.Web.Services
{
    public abstract class BaseApiService
    {
        protected readonly HttpClient _httpClient;

        public BaseApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("TaskFlowApi");
        }

        protected async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new Exception(problem?.Detail ?? "Request failed");
        }

        protected async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new Exception(problem?.Detail ?? "Request failed");
        }

        protected async Task DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem?.Detail ?? "Request failed");
            }
        }

        protected async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new Exception(problem?.Detail ?? "Request failed");
        }
    }
}
