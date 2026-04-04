
namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    class InsuranceCompanyService
    {
        private readonly HttpClient _httpClient;

        public InsuranceCompanyService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task AddInsuranceCompanyAsync(AddInsuranceCompanyDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.MasterTable.InsuranceComp.AddInsuranceComp, dto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Saved Successfully!");

            }
            else
            {
                MessageBox.Show("Error: Could not save data to the server.");
            }
        }

        public async Task UpdateInsuranceCompanyAsync(UpdateInsuranceCompanyDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.MasterTable.InsuranceComp.UpdateInsuranceComp, dto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Updated Successfully!");

            }
            else
            {
                MessageBox.Show("Error: Could not update data to the server.");
            }
        }

        public async  Task<List<GetInsuranceCompanyDTO>> GetAllInsuranceCompaniesAsync()
        {

            var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.InsuranceComp.GetAllInsuranceCompanies);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");
            }

            return await response.Content.ReadFromJsonAsync<List<GetInsuranceCompanyDTO>>();
        }

        public async Task<GetInsuranceCompanyDTO> GetInsuranceCompanyByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"{ ApiRoutes.MasterTable.InsuranceComp.GetInsuranceCompanyByName}?Name={name}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show("No insurance company found with the specified name.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
                return null; 
            }
            return await response.Content.ReadFromJsonAsync<GetInsuranceCompanyDTO>();

        }
    }
}
