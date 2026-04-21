

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class PolicyTypeApiService
    {
        private readonly HttpClientService _httpClientService;

        public PolicyTypeApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetPolicyTypeDTO>>> GetAllPolicyTypesAsync()
        {
            return await _httpClientService.GetAsync<List<GetPolicyTypeDTO>>(ApiRoutes.MasterTable.PolicyType.GetAllPolicyTypes);
        }

        public async Task<ApiResponse<GetPolicyTypeDTO>> GetPolicyTypeByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetPolicyTypeDTO>($"{ApiRoutes.MasterTable.PolicyType.GetPolicyTypeById}?id={id}");
        }

        public async Task<ApiResponse<GetPolicyTypeDTO>> AddPolicyTypeAsync(AddPolicyTypeDTO dto)
        {
            return await _httpClientService.PostAsync<GetPolicyTypeDTO, AddPolicyTypeDTO>(ApiRoutes.MasterTable.PolicyType.AddPolicyType, dto);
        }

        public async Task<ApiResponse<GetPolicyTypeDTO>> UpdatePolicyTypeAsync(UpdatePolicyTypeDTO dto)
        {
            return await _httpClientService.PutAsync<GetPolicyTypeDTO, UpdatePolicyTypeDTO>(ApiRoutes.MasterTable.PolicyType.UpdatePolicyType, dto);
        }

        public async Task<ApiResponse<string>> DeletePolicyTypeAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.PolicyType.DeletePolicyType}?id={id}");
        }
    }
}
