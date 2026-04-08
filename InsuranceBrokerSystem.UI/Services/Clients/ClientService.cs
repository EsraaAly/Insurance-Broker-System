using InsuranceBrokerSystem.Application.DTOs.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InsuranceBrokerSystem.UI.Services.Clients
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44314";

        public ClientService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ApiResponse<List<GetClientDTO>>> GetAllClientsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.Clients.GetAllClients);
                return await ApiResponseHandler.HandleResponseAsync<List<GetClientDTO>>(response);
            }
            catch (HttpRequestException ex)
            {
                ApiResponseHandler.ShowError($"Network error: {ex.Message}");
                return ApiResponse<List<GetClientDTO>>.Failure("Network connection failed");
            }
            catch (TaskCanceledException ex)
            {
                ApiResponseHandler.ShowError($"Request timeout: {ex.Message}");
                return ApiResponse<List<GetClientDTO>>.Failure("Request timed out");
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Unexpected error: {ex.Message}");
                return ApiResponse<List<GetClientDTO>>.Failure("Unexpected error occurred");
            }
        }

        public async Task<ApiResponse<GetClientDTO>> GetClientByIdAsync(int id)
        {
            try
            {
                var url = ApiRoutes.Clients.GetClientById.Replace("{id}", id.ToString());
                var response = await _httpClient.GetAsync(url);
                return await ApiResponseHandler.HandleResponseAsync<GetClientDTO>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving client: {ex.Message}");
                return ApiResponse<GetClientDTO>.Failure("Failed to retrieve client");
            }
        }

        public async Task<ApiResponse<GetClientDTO>> AddClientAsync(AddClientDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.Clients.AddClient, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetClientDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Client added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding client: {ex.Message}");
                return ApiResponse<GetClientDTO>.Failure("Failed to add client");
            }
        }

        public async Task<ApiResponse<GetClientDTO>> UpdateClientAsync(UpdateClientDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.Clients.UpdateClient, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetClientDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Client updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating client: {ex.Message}");
                return ApiResponse<GetClientDTO>.Failure("Failed to update client");
            }
        }

        public async Task<ApiResponse<string>> DeleteClientAsync(int id)
        {
            try
            {
                var url = ApiRoutes.Clients.DeleteClient.Replace("{id}", id.ToString());
                var response = await _httpClient.DeleteAsync(url);
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Client deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting client: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete client");
            }
        }

        public async Task<ApiResponse<GetClientDTO>> ApproveClientAsync(int id)
        {
            try
            {
                var url = ApiRoutes.Clients.ApproveClient.Replace("{id}", id.ToString());
                var content = JsonContent.Create(new { });
                var response = await _httpClient.PostAsync(url, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetClientDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Client approved successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error approving client: {ex.Message}");
                return ApiResponse<GetClientDTO>.Failure("Failed to approve client");
            }
        }

        public async Task<ApiResponse<GetClientDTO>> RejectClientAsync(int id)
        {
            try
            {
                var url = ApiRoutes.Clients.RejectClient.Replace("{id}", id.ToString());
                var content = JsonContent.Create(new { });
                var response = await _httpClient.PostAsync(url, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetClientDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Client rejected successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error rejecting client: {ex.Message}");
                return ApiResponse<GetClientDTO>.Failure("Failed to reject client");
            }
        }

        public async Task<ApiResponse<GetClientDTO>> BlockClientAsync(int id)
        {
            try
            {
                var url = ApiRoutes.Clients.BlockClient.Replace("{id}", id.ToString());
                var content = JsonContent.Create(new { });
                var response = await _httpClient.PostAsync(url, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetClientDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Client blocked successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error blocking client: {ex.Message}");
                return ApiResponse<GetClientDTO>.Failure("Failed to block client");
            }
        }
    }
}
