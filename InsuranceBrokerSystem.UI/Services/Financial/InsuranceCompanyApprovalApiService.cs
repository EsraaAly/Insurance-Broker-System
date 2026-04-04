
namespace InsuranceBrokerSystem.UI.Services.Financial
{
    public class InsuranceCompanyApprovalApiService
    {
        private readonly HttpClient _httpClient;

        public InsuranceCompanyApprovalApiService() 
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task ApproveInsuranceCompanyAsync(int id)
        {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Financial.InsuranceComp.ApproveInsuranceComp, id);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");

            }

        }

        public async Task RejectInsuranceCompanyAsync(int id)
        {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Financial.InsuranceComp.RejectInsuranceComp, id);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");

            }

        }
    }
}
