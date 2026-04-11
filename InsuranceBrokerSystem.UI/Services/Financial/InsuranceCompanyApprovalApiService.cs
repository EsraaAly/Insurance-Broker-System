
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Financial
{
    public class InsuranceCompanyApprovalApiService
    {
        private readonly HttpClientService _httpClientService;

        public InsuranceCompanyApprovalApiService(HttpClientService httpClientService) 
        {
            _httpClientService = httpClientService;
        }

        public async Task ApproveInsuranceCompanyAsync(int id)
        {
            await _httpClientService.PutAsync<string, object>(ApiRoutes.Financial.InsuranceComp.ApproveInsuranceComp, id);
        }

        public async Task RejectInsuranceCompanyAsync(int id)
        {
            await _httpClientService.PutAsync<string, object>(ApiRoutes.Financial.InsuranceComp.RejectInsuranceComp, id);
        }
    }
}
