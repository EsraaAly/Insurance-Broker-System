using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
using InsuranceBrokerSystem.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceContactService
    {
        private readonly HttpClientService _httpClientService;

        public InsuranceContactService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetInsuranceContractDTO>>> GetInsuranceContactByInsuranceIdAsync(int id)
        {
            return await _httpClientService.GetAsync<List<GetInsuranceContractDTO>>($"{ ApiRoutes.MasterTable.InsuranceCompContact.GetInsuranceContactByInsuranceIdAsync}/{id}");
        }
    }
}
