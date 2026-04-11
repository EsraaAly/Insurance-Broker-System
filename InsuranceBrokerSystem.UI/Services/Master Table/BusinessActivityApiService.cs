using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class BusinessActivityApiService
    {
        private readonly HttpClientService _httpClientService;

        public BusinessActivityApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetBusinessActivityDTO>>> GetAllBusinessActivitiesAsync()
        {
            return await _httpClientService.GetAsync<List<GetBusinessActivityDTO>>(ApiRoutes.MasterTable.BusinessActivity.GetAllBusinessActivities);
        }

        public async Task<ApiResponse<GetBusinessActivityDTO>> GetBusinessActivityByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetBusinessActivityDTO>($"{ApiRoutes.MasterTable.BusinessActivity.GetBusinessActivityById}?id={id}");
        }

        public async Task<ApiResponse<GetBusinessActivityDTO>> AddBusinessActivityAsync(AddBusinessActivityDTO dto)
        {
            return await _httpClientService.PostAsync<GetBusinessActivityDTO, AddBusinessActivityDTO>(ApiRoutes.MasterTable.BusinessActivity.AddBusinessActivity, dto);
        }

        public async Task<ApiResponse<GetBusinessActivityDTO>> UpdateBusinessActivityAsync(UpdateBusinessActivityDTO dto)
        {
            return await _httpClientService.PutAsync<GetBusinessActivityDTO, UpdateBusinessActivityDTO>(ApiRoutes.MasterTable.BusinessActivity.UpdateBusinessActivity, dto);
        }

        public async Task<ApiResponse<string>> DeleteBusinessActivityAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.BusinessActivity.DeleteBusinessActivity}?id={id}");
        }
    }

    // DTO Classes - These should match the Application layer DTOs
    public class GetBusinessActivityDTO
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddBusinessActivityDTO
    {
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string Description { get; set; }
    }

    public class UpdateBusinessActivityDTO
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
