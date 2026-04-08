using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace InsuranceBrokerSystem.UI.Services
{
    public static class ApiResponseHandler
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<ApiResponse<T>> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<T>(content, _jsonOptions);
                    return ApiResponse<T>.Success(data);
                }
                else
                {
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content, _jsonOptions);
                    var errorMessage = errorResponse?.Message ?? GetDefaultErrorMessage(response.StatusCode);
                    return ApiResponse<T>.Failure(errorMessage, response.StatusCode);
                }
            }
            catch (JsonException ex)
            {
                return ApiResponse<T>.Failure($"JSON parsing error: {ex.Message}", response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.Failure($"Unexpected error: {ex.Message}", response.StatusCode);
            }
        }

        public static void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowSuccess(string message, string title = "Success")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static string GetDefaultErrorMessage(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "Bad request. Please check your input and try again.",
                HttpStatusCode.Unauthorized => "Access denied. You don't have permission to perform this action.",
                HttpStatusCode.Forbidden => "Access forbidden. This action is not allowed.",
                HttpStatusCode.NotFound => "Resource not found. The requested item could not be found.",
                HttpStatusCode.InternalServerError => "Server error. Please try again later.",
                HttpStatusCode.ServiceUnavailable => "Service unavailable. Please try again later.",
                _ => $"An error occurred. Status code: {(int)statusCode}"
            };
        }
    }

    public class ApiResponse<T>
    {
        public bool Successed { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode? StatusCode { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { Successed = true, Data = data };
        }

        public static ApiResponse<T> Failure(string message, HttpStatusCode? statusCode = null)
        {
            return new ApiResponse<T> { Successed = false, Message = message, StatusCode = statusCode };
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}
