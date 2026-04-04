
namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceClassApiService
    {
        private readonly HttpClient _httpClient;
        
        public InsuranceClassApiService()
        {
            _httpClient = new HttpClient{
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<List<GetInsuranceClassDTO>> GetAllClassesAsync()
        {
            try
            {
                // This sends a GET to https://localhost:7001/api/InsuranceClass
                return await _httpClient.GetFromJsonAsync<List<GetInsuranceClassDTO>>(ApiRoutes.MasterTable.InsuranceClass.GetAllInsuranceClasses);
            }
            catch (HttpRequestException)
            {
                // Handle connection errors (API is offline, etc.)
                return new List<GetInsuranceClassDTO>();
            }
        }

        public async Task<bool> AddClassAsync(AddInsuranceClassDTO newClass)
        {
            try
            {

                // This sends a GET to https://localhost:7001/api/InsuranceClass
                //return await _httpClient.GetFromJsonAsync<List<GetInsuranceClassDTO>>("/api/InsuranceClass/AddClassAsync");

                // Use PostAsJsonAsync to send the DTO
                var response = await _httpClient.PostAsJsonAsync(ApiRoutes.MasterTable.InsuranceClass.AddInsuranceClass, newClass);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Saved Successfully!");
                    return true;

                }
                else
                {
                    MessageBox.Show("Error: Could not save data to the server.");
                }
                return false;


            }
            catch (HttpRequestException)
            {
                // Handle connection errors (API is offline, etc.)
                return false;
            }
        }

        public async Task UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            try
            {
                // Use PostAsJsonAsync to send the DTO
                var response = await _httpClient.PutAsJsonAsync(ApiRoutes.MasterTable.InsuranceClass.UpdateInsuranceClass, dto);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Updated Successfully!");

                }
                else
                {
                    MessageBox.Show("Error: Could not update data to the server.");
                }


            }
            catch (HttpRequestException)
            {
                // Handle connection errors (API is offline, etc.)
            }
        }

        public async Task DeleteClassAsync( int id)
        {
            try
            {

                // This sends a GET to https://localhost:7001/api/InsuranceClass
                //return await _httpClient.GetFromJsonAsync<List<GetInsuranceClassDTO>>("/api/InsuranceClass/AddClassAsync");

                // Use PostAsJsonAsync to send the DTO
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.InsuranceClass.DeleteInsuranceClass}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Deleted Successfully!");

                }
                else
                {
                    MessageBox.Show("Error: Could not delete data to the server.");
                }


            }
            catch (HttpRequestException)
            {
                // Handle connection errors (API is offline, etc.)
            }
        }
    }
}
