using InsuranceBrokerSystem.Application.DTOs.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace InsuranceBrokerSystem.UI.Services.Clients
{
    public class ClientServiceold
    {
        private readonly HttpClient _httpClient;

        public ClientServiceold()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<GetClientDTO> AddClientAsync(AddClientDTO dto)
        {
            var result = await _httpClient.PostAsJsonAsync(ApiRoutes.Clients.AddClient, dto);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync< ApiResponse<GetClientDTO>>();
                MessageBox.Show("Client saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return response.Data;
            }
            else
            {
                MessageBox.Show("Error: Could not save client data to the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task<GetClientDTO> UpdateClientAsync(UpdateClientDTO dto)
        {
            var result = await _httpClient.PutAsJsonAsync(ApiRoutes.Clients.UpdateClient, dto);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<ApiResponse<GetClientDTO>>();
                MessageBox.Show("Client updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return response.Data;
            }
            else
            {
                MessageBox.Show("Error: Could not update client data to the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task<List<GetClientDTO>> GetAllClientsAsync()
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Clients.GetAllClients);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GetClientDTO>>() ?? new();
            }
            else
            {
                // قراءة الرسالة اللي بعتها الـ BadRequest(result.Message)
                var errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"API Error: {errorMessage}", "Stop", MessageBoxButton.OK, MessageBoxImage.Warning);
                return new List<GetClientDTO>();
            }
        }

        public async Task<GetClientDTO> GetClientByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Clients.GetClientById.Replace("{id}", id.ToString()));

            if (response.IsSuccessStatusCode)
            {
                var client = await response.Content.ReadFromJsonAsync<GetClientDTO>();
                return client;
            }
            else
            {
                MessageBox.Show("Error: Could not retrieve client from the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(ApiRoutes.Clients.DeleteClient.Replace("{id}", id.ToString()));

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Client deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Error: Could not delete client from the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public async Task<GetClientDTO> ApproveClientAsync(int id)
        {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Clients.ApproveClient, id);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetClientDTO>();
                MessageBox.Show("Client approved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return result;
            }
            else
            {
                MessageBox.Show("Error: Could not approve client.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task<GetClientDTO> RejectClientAsync(int id, string rejectedBy)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Clients.RejectClient.Replace("{id}", id.ToString()), rejectedBy);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetClientDTO>();
                MessageBox.Show("Client rejected successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return result;
            }
            else
            {
                MessageBox.Show("Error: Could not reject client.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task<GetClientDTO> BlockClientAsync(int id, string blockedBy)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Clients.BlockClient.Replace("{id}", id.ToString()), blockedBy);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetClientDTO>();
                MessageBox.Show("Client blocked successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return result;
            }
            else
            {
                MessageBox.Show("Error: Could not block client.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
