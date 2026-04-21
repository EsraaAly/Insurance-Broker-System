using InsuranceBrokerSystem.UI;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class PositionApiService
    {
        private readonly HttpClientService _httpClientService;

        public PositionApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetPositionDTO>>> GetAllPositionsAsync()
        {
            return await _httpClientService.GetAsync<List<GetPositionDTO>>(ApiRoutes.MasterTable.Position.GetAllPositions);
        }

        public async Task<ApiResponse<GetPositionDTO>> GetPositionByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetPositionDTO>($"{ApiRoutes.MasterTable.Position.GetPositionById}?id={id}");
        }

        public async Task<ApiResponse<GetPositionDTO>> AddPositionAsync(AddPositionDTO dto)
        {
            return await _httpClientService.PostAsync<GetPositionDTO, AddPositionDTO>(ApiRoutes.MasterTable.Position.AddPosition, dto);
        }

        public async Task<ApiResponse<GetPositionDTO>> UpdatePositionAsync(UpdatePositionDTO dto)
        {
            return await _httpClientService.PutAsync<GetPositionDTO, UpdatePositionDTO>(ApiRoutes.MasterTable.Position.UpdatePosition, dto);
        }

        public async Task<ApiResponse<string>> DeletePositionAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.Position.DeletePosition}?id={id}");
        }
    }
}
