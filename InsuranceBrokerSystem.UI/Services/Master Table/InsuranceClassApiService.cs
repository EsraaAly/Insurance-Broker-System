using InsuranceBrokerSystem.Application.DTOs.Master_Table;
using InsuranceBrokerSystem.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceClassApiService
    {
        private readonly HttpClientService _httpClientService;

        public InsuranceClassApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetInsuranceClassDTO>>> GetAllClassesAsync()
        {
            return await _httpClientService.GetAsync<List<GetInsuranceClassDTO>>(ApiRoutes.MasterTable.InsuranceClass.GetAllInsuranceClasses);
        }

        public async Task<ApiResponse<GetInsuranceClassDTO>> AddClassAsync(AddInsuranceClassDTO newClass)
        {
            return await _httpClientService.PostAsync<GetInsuranceClassDTO, AddInsuranceClassDTO>(ApiRoutes.MasterTable.InsuranceClass.AddInsuranceClass, newClass);
        }

        public async Task<ApiResponse<GetInsuranceClassDTO>> UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            return await _httpClientService.PutAsync<GetInsuranceClassDTO, UpdateInsuranceClassDTO>(ApiRoutes.MasterTable.InsuranceClass.UpdateInsuranceClass, dto);
        }

        public async Task<ApiResponse<string>> DeleteClassAsync(int id)
        {
            var url = ApiRoutes.MasterTable.InsuranceClass.DeleteInsuranceClass.Replace("{id}", id.ToString());
            return await _httpClientService.DeleteAsync(url);
        }
    }
}
