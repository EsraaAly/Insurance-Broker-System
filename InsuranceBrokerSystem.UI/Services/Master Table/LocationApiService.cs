using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class LocationApiService
    {
        private readonly HttpClientService _httpClientService;

        public LocationApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetLocationDTO>>> GetAllLocationsAsync()
        {
            return await _httpClientService.GetAsync<List<GetLocationDTO>>(ApiRoutes.MasterTable.Location.GetAllLocations);
        }

        public async Task<ApiResponse<GetLocationDTO>> GetLocationByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetLocationDTO>($"{ApiRoutes.MasterTable.Location.GetLocationById}?id={id}");
        }

        public async Task<ApiResponse<GetLocationDTO>> AddLocationAsync(AddLocationDTO dto)
        {
            return await _httpClientService.PostAsync<GetLocationDTO, AddLocationDTO>(ApiRoutes.MasterTable.Location.AddLocation, dto);
        }

        public async Task<ApiResponse<GetLocationDTO>> UpdateLocationAsync(UpdateLocationDTO dto)
        {
            return await _httpClientService.PutAsync<GetLocationDTO, UpdateLocationDTO>(ApiRoutes.MasterTable.Location.UpdateLocation, dto);
        }

        public async Task<ApiResponse<string>> DeleteLocationAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.Location.DeleteLocation}?id={id}");
        }
    }

}
