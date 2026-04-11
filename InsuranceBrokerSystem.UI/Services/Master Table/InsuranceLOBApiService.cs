using InsuranceBrokerSystem.Application.DTOs.Master_Table;
using InsuranceBrokerSystem.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceLOBApiService
    {
        private readonly HttpClientService _httpClientService;

        public InsuranceLOBApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetInsuranceLOBDTO>>> GetAllLOBAsync()
        {
            return await _httpClientService.GetAsync<List<GetInsuranceLOBDTO>>(ApiRoutes.MasterTable.InsuranceLOB.GetAllInsuranceLOBs);
        }

        public async Task<ApiResponse<List<GetInsuranceLOBDTO>>> GetLOBByClassIdAsync(int classId)
        {
            var url = ApiRoutes.MasterTable.InsuranceLOB.GetLOBByClassIdAsync.Replace("{id}", classId.ToString());
            return await _httpClientService.GetAsync<List<GetInsuranceLOBDTO>>(url);
        }

        public async Task<ApiResponse<GetInsuranceLOBDTO>> AddLOBAsync(AddInsuranceLOBDTO dto)
        {
            return await _httpClientService.PostAsync<GetInsuranceLOBDTO, AddInsuranceLOBDTO>(ApiRoutes.MasterTable.InsuranceLOB.AddInsuranceLOB, dto);
        }

        public async Task<ApiResponse<string>> DeleteLOBAsync(int id)
        {
            var url = ApiRoutes.MasterTable.InsuranceLOB.DeleteInsuranceLOB.Replace("{id}", id.ToString());
            return await _httpClientService.DeleteAsync(url);
        }

        public async Task<ApiResponse<GetInsuranceLOBDTO>> UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            return await _httpClientService.PutAsync<GetInsuranceLOBDTO, UpdateInsuranceLOBDTO>(ApiRoutes.MasterTable.InsuranceLOB.UpdateInsuranceLOB, dto);
        }
    }
}
