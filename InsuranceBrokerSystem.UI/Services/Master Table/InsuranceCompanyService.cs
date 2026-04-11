
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceCompanyService
    {
        private readonly HttpClientService _httpClientService;

        public InsuranceCompanyService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<GetInsuranceCompanyDTO>> AddInsuranceCompanyAsync(AddInsuranceCompanyDTO dto)
        {
            return await _httpClientService.PostAsync<GetInsuranceCompanyDTO, AddInsuranceCompanyDTO>(ApiRoutes.MasterTable.InsuranceComp.AddInsuranceComp, dto);
        }

        public async Task<ApiResponse<GetInsuranceCompanyDTO>> UpdateInsuranceCompanyAsync(UpdateInsuranceCompanyDTO dto)
        {
            return await _httpClientService.PutAsync<GetInsuranceCompanyDTO, UpdateInsuranceCompanyDTO>(ApiRoutes.MasterTable.InsuranceComp.UpdateInsuranceComp, dto);
        }

        public async Task<ApiResponse<List<GetInsuranceCompanyDTO>>> GetAllInsuranceCompaniesAsync()
        {
            return await _httpClientService.GetAsync<List<GetInsuranceCompanyDTO>>(ApiRoutes.MasterTable.InsuranceComp.GetAllInsuranceCompanies);
        }

        public async Task<ApiResponse<GetInsuranceCompanyDTO>> GetInsuranceCompanyByNameAsync(string name)
        {
            var response = await _httpClientService.GetAsync<GetInsuranceCompanyDTO>($"{ApiRoutes.MasterTable.InsuranceComp.GetInsuranceCompanyByName}?Name={name}");
            
            if (!response.Successed && response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ApiResponseHandler.ShowError("No insurance company found with the specified name.", "Not Found");
            }
            
            return response;
        }

        public async Task<ApiResponse<string>> DeleteInsuranceCompanyAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.InsuranceComp.DeleteInsuranceComp}?id={id}");
        }
    }
}
