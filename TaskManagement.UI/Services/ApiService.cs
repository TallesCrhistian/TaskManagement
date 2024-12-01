namespace TaskManagement.UI.Services
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using TaskManagement.UI.DTOs;
    using TaskManagement.UI.Entities;
    using TaskManagement.UI.ViewModels;

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ServiceResponseDTO<ListResponseDTO<TResponse>>> GetListAsync<TResponse, TRequest>(
          TRequest filters,
          int pageIndex,
          string urlBase)
        {
            try
            {               
                var queryParameters = new StringBuilder();
                queryParameters.Append($"?pageIndex={pageIndex}");
               
                if (filters != null)
                {                    
                    var filterProperties = filters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    
                    foreach (var property in filterProperties)
                    {
                        var value = property.GetValue(filters);
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            queryParameters.Append($"&{property.Name}={value}");
                        }
                    }
                }

                string url = $"{urlBase}/List{queryParameters.ToString()}";

                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _httpClient.GetAsync(url);               
               
                string responseBody = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponseDTO<ListResponseDTO<TResponse>>>(responseBody);

                return serviceResponse;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(string url, TRequest requestData)
        {
            try
            {                
                var jsonRequest = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                string responseBody = await response.Content.ReadAsStringAsync();
                
                response.EnsureSuccessStatusCode();
               
                TResponse responseObject = JsonConvert.DeserializeObject<TResponse>(responseBody);

                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consumir a API: {ex.Message}");
            }
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string url, Guid id)
        {
            try
            {               
                string requestUrl = $"{url}?id={id}";


                HttpResponseMessage response = await _httpClient.DeleteAsync(requestUrl);
                
                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();
               
                TResponse result = JsonConvert.DeserializeObject<TResponse>(responseBody);
                
                return result;
            }
            catch (HttpRequestException httpRequestException)
            {                
                throw new Exception("Erro ao realizar a requisição DELETE.", httpRequestException);
            }
            catch (Exception ex)
            {                
                throw new Exception("Erro ao processar a resposta da API.", ex);
            }
        }
    }
}
