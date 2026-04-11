using InsuranceBrokerSystem.Application.DTOs.Client;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Clients
{
    public class ClientService
    {
        private readonly HttpClientService _httpClientService;

        public ClientService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetClientDTO>>> GetAllClientsAsync()
        {
            return await _httpClientService.GetAsync<List<GetClientDTO>>(ApiRoutes.Clients.GetAllClients);
        }

        public async Task<ApiResponse<GetClientDTO>> GetClientByIdAsync(int id)
        {
            var url = ApiRoutes.Clients.GetClientById.Replace("{id}", id.ToString());
            return await _httpClientService.GetAsync<GetClientDTO>(url);
        }

        public async Task<ApiResponse<GetClientDTO>> AddClientAsync(AddClientDTO dto)
        {
            return await _httpClientService.PostAsync<GetClientDTO, AddClientDTO>(ApiRoutes.Clients.AddClient, dto);
        }

        public async Task<ApiResponse<GetClientDTO>> UpdateClientAsync(UpdateClientDTO dto)
        {
            return await _httpClientService.PutAsync<GetClientDTO, UpdateClientDTO>(ApiRoutes.Clients.UpdateClient, dto);
        }

        public async Task<ApiResponse<string>> DeleteClientAsync(int id)
        {
            var url = ApiRoutes.Clients.DeleteClient.Replace("{id}", id.ToString());
            return await _httpClientService.DeleteAsync(url);
        }

        public async Task<ApiResponse<GetClientDTO>> ApproveClientAsync(int id)
        {
            var url = ApiRoutes.Clients.ApproveClient.Replace("{id}", id.ToString());
            return await _httpClientService.PostAsync<GetClientDTO, object>(url, new { });
        }

        public async Task<ApiResponse<GetClientDTO>> RejectClientAsync(int id)
        {
            var url = ApiRoutes.Clients.RejectClient.Replace("{id}", id.ToString());
            return await _httpClientService.PostAsync<GetClientDTO, object>(url, new { });
        }

        public async Task<ApiResponse<GetClientDTO>> BlockClientAsync(int id)
        {
            var url = ApiRoutes.Clients.BlockClient.Replace("{id}", id.ToString());
            return await _httpClientService.PostAsync<GetClientDTO, object>(url, new { });
        }
    }
}
