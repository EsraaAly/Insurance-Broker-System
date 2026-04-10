using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class PolicyTypeApiService
    {
        private readonly HttpClient _httpClient;

        public PolicyTypeApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<ApiResponse<List<GetPolicyTypeDTO>>> GetAllPolicyTypesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.PolicyType.GetAllPolicyTypes);
                return await ApiResponseHandler.HandleResponseAsync<List<GetPolicyTypeDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving policy types: {ex.Message}");
                return ApiResponse<List<GetPolicyTypeDTO>>.Failure("Failed to retrieve policy types");
            }
        }

        public async Task<ApiResponse<GetPolicyTypeDTO>> GetPolicyTypeByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.PolicyType.GetPolicyTypeById}?id={id}");
                return await ApiResponseHandler.HandleResponseAsync<GetPolicyTypeDTO>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving policy type: {ex.Message}");
                return ApiResponse<GetPolicyTypeDTO>.Failure("Failed to retrieve policy type");
            }
        }

        public async Task<ApiResponse<GetPolicyTypeDTO>> AddPolicyTypeAsync(AddPolicyTypeDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.PolicyType.AddPolicyType, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetPolicyTypeDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Policy type added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding policy type: {ex.Message}");
                return ApiResponse<GetPolicyTypeDTO>.Failure("Failed to add policy type");
            }
        }

        public async Task<ApiResponse<GetPolicyTypeDTO>> UpdatePolicyTypeAsync(UpdatePolicyTypeDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.PolicyType.UpdatePolicyType, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetPolicyTypeDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Policy type updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating policy type: {ex.Message}");
                return ApiResponse<GetPolicyTypeDTO>.Failure("Failed to update policy type");
            }
        }

        public async Task<ApiResponse<string>> DeletePolicyTypeAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.PolicyType.DeletePolicyType}?id={id}");
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Policy type deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting policy type: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete policy type");
            }
        }
    }

    // DTO Classes
    public class GetPolicyTypeDTO
    {
        public int Id { get; set; }
        public string PolicyTypeName { get; set; }
        public string PolicyTypeNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddPolicyTypeDTO
    {
        public string PolicyTypeName { get; set; }
        public string PolicyTypeNameAr { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePolicyTypeDTO
    {
        public int Id { get; set; }
        public string PolicyTypeName { get; set; }
        public string PolicyTypeNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
