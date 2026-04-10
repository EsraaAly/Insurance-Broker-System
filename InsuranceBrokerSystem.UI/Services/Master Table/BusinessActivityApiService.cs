using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class BusinessActivityApiService
    {
        private readonly HttpClient _httpClient;

        public BusinessActivityApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<ApiResponse<List<GetBusinessActivityDTO>>> GetAllBusinessActivitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.BusinessActivity.GetAllBusinessActivities);
                return await ApiResponseHandler.HandleResponseAsync<List<GetBusinessActivityDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving business activities: {ex.Message}");
                return ApiResponse<List<GetBusinessActivityDTO>>.Failure("Failed to retrieve business activities");
            }
        }

        public async Task<ApiResponse<GetBusinessActivityDTO>> GetBusinessActivityByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.BusinessActivity.GetBusinessActivityById}?id={id}");
                return await ApiResponseHandler.HandleResponseAsync<GetBusinessActivityDTO>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving business activity: {ex.Message}");
                return ApiResponse<GetBusinessActivityDTO>.Failure("Failed to retrieve business activity");
            }
        }

        public async Task<ApiResponse<GetBusinessActivityDTO>> AddBusinessActivityAsync(AddBusinessActivityDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.BusinessActivity.AddBusinessActivity, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetBusinessActivityDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Business activity added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding business activity: {ex.Message}");
                return ApiResponse<GetBusinessActivityDTO>.Failure("Failed to add business activity");
            }
        }

        public async Task<ApiResponse<GetBusinessActivityDTO>> UpdateBusinessActivityAsync(UpdateBusinessActivityDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.BusinessActivity.UpdateBusinessActivity, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetBusinessActivityDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Business activity updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating business activity: {ex.Message}");
                return ApiResponse<GetBusinessActivityDTO>.Failure("Failed to update business activity");
            }
        }

        public async Task<ApiResponse<string>> DeleteBusinessActivityAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.BusinessActivity.DeleteBusinessActivity}?id={id}");
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Business activity deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting business activity: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete business activity");
            }
        }
    }

    // DTO Classes - These should match the Application layer DTOs
    public class GetBusinessActivityDTO
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddBusinessActivityDTO
    {
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string Description { get; set; }
    }

    public class UpdateBusinessActivityDTO
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string ActivityNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
