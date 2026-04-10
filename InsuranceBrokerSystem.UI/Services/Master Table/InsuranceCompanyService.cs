
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceCompanyService
    {
        private readonly HttpClient _httpClient;

        public InsuranceCompanyService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<ApiResponse<GetInsuranceCompanyDTO>> AddInsuranceCompanyAsync(AddInsuranceCompanyDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.InsuranceComp.AddInsuranceComp, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceCompanyDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance company added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding insurance company: {ex.Message}");
                return ApiResponse<GetInsuranceCompanyDTO>.Failure("Failed to add insurance company");
            }
        }

        public async Task<ApiResponse<GetInsuranceCompanyDTO>> UpdateInsuranceCompanyAsync(UpdateInsuranceCompanyDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.InsuranceComp.UpdateInsuranceComp, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceCompanyDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance company updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating insurance company: {ex.Message}");
                return ApiResponse<GetInsuranceCompanyDTO>.Failure("Failed to update insurance company");
            }
        }

        public async Task<ApiResponse<List<GetInsuranceCompanyDTO>>> GetAllInsuranceCompaniesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.InsuranceComp.GetAllInsuranceCompanies);
                return await ApiResponseHandler.HandleResponseAsync<List<GetInsuranceCompanyDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving insurance companies: {ex.Message}");
                return ApiResponse<List<GetInsuranceCompanyDTO>>.Failure("Failed to retrieve insurance companies");
            }
        }

        public async Task<ApiResponse<GetInsuranceCompanyDTO>> GetInsuranceCompanyByNameAsync(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.InsuranceComp.GetInsuranceCompanyByName}?Name={name}");
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceCompanyDTO>(response);
                
                if (!result.Successed && result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ApiResponseHandler.ShowError("No insurance company found with the specified name.", "Not Found");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving insurance company: {ex.Message}");
                return ApiResponse<GetInsuranceCompanyDTO>.Failure("Failed to retrieve insurance company");
            }
        }

        public async Task<ApiResponse<string>> DeleteInsuranceCompanyAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.InsuranceComp.DeleteInsuranceComp}?id={id}");
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance company deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting insurance company: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete insurance company");
            }
        }
    }
}
