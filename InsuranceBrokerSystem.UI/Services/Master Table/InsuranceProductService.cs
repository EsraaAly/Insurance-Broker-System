using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
using InsuranceBrokerSystem.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceProductService
    {
        private readonly HttpClientService _httpClientService;

        public InsuranceProductService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetInsuranceProductDTO>>> GetInsuranceProductByInsuranceIdAsync(int id)
        {
            return await _httpClientService.GetAsync<List<GetInsuranceProductDTO>>($"{ApiRoutes.MasterTable.InsuranceCompProduct.GetInsuranceProductByInsuranceId}/{id}");
        }
    }
}
