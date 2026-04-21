using InsuranceBrokerSystem.UI;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace InsuranceBrokerSystem.UI.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44314";

        public HttpClientService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                var result = await ApiResponseHandler.HandleResponseAsync<T>(response);
                
                // Don't show error messages for 405 Method Not Allowed since we have fallback data
                if (!result.Successed && result.StatusCode != HttpStatusCode.MethodNotAllowed)
                {
                    ApiResponseHandler.ShowError(result.Message);
                }
                
                return result;

            }
            catch (HttpRequestException ex)
            {
                ApiResponseHandler.ShowError($"Network error: {ex.Message}");
                return ApiResponse<T>.Failure("Network connection failed");
            }
            catch (TaskCanceledException ex)
            {
                ApiResponseHandler.ShowError($"Request timeout: {ex.Message}");
                return ApiResponse<T>.Failure("Request timed out");
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Unexpected error: {ex.Message}");
                return ApiResponse<T>.Failure("Unexpected error occurred");
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T, TRequest>(string endpoint, TRequest data)
        {
            try
            {
                var content = JsonContent.Create(data, options: new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var response = await _httpClient.PostAsync(endpoint, content);

                var result = await ApiResponseHandler.HandleResponseAsync<T>(response);

                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Operation completed successfully!");
                }
                
                return result;
            }
            catch (HttpRequestException ex)
            {
                ApiResponseHandler.ShowError($"Network error: {ex.Message}");
                return ApiResponse<T>.Failure("Network connection failed");
            }
            catch (TaskCanceledException ex)
            {
                ApiResponseHandler.ShowError($"Request timeout: {ex.Message}");
                return ApiResponse<T>.Failure("Request timed out");
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Unexpected error: {ex.Message}");
                return ApiResponse<T>.Failure("Unexpected error occurred");
            }
        }

        public async Task<ApiResponse<T>> PutAsync<T, TRequest>(string endpoint, TRequest data)
        {
            try
            {
                var content = JsonContent.Create(data);
                var response = await _httpClient.PutAsync(endpoint, content);
                var result = await ApiResponseHandler.HandleResponseAsync<T>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Operation completed successfully!");
                }
                
                return result;
            }
            catch (HttpRequestException ex)
            {
                ApiResponseHandler.ShowError($"Network error: {ex.Message}");
                return ApiResponse<T>.Failure("Network connection failed");
            }
            catch (TaskCanceledException ex)
            {
                ApiResponseHandler.ShowError($"Request timeout: {ex.Message}");
                return ApiResponse<T>.Failure("Request timed out");
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Unexpected error: {ex.Message}");
                return ApiResponse<T>.Failure("Unexpected error occurred");
            }
        }

        public async Task<ApiResponse<string>> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Item deleted successfully!");
                }
                
                return result;
            }
            catch (HttpRequestException ex)
            {
                ApiResponseHandler.ShowError($"Network error: {ex.Message}");
                return ApiResponse<string>.Failure("Network connection failed");
            }
            catch (TaskCanceledException ex)
            {
                ApiResponseHandler.ShowError($"Request timeout: {ex.Message}");
                return ApiResponse<string>.Failure("Request timed out");
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Unexpected error: {ex.Message}");
                return ApiResponse<string>.Failure("Unexpected error occurred");
            }
        }
    }
}
