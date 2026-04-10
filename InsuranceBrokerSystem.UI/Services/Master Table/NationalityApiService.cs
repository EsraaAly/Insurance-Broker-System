using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class NationalityApiService
    {
        private readonly HttpClient _httpClient;

        public NationalityApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<ApiResponse<List<GetNationalityDTO>>> GetAllNationalitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.Nationality.GetAllNationalities);
                return await ApiResponseHandler.HandleResponseAsync<List<GetNationalityDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving nationalities: {ex.Message}");
                return ApiResponse<List<GetNationalityDTO>>.Failure("Failed to retrieve nationalities");
            }
        }

        public async Task<ApiResponse<GetNationalityDTO>> GetNationalityByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.Nationality.GetNationalityById}?id={id}");
                return await ApiResponseHandler.HandleResponseAsync<GetNationalityDTO>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving nationality: {ex.Message}");
                return ApiResponse<GetNationalityDTO>.Failure("Failed to retrieve nationality");
            }
        }

        public async Task<ApiResponse<GetNationalityDTO>> AddNationalityAsync(AddNationalityDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.Nationality.AddNationality, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetNationalityDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Nationality added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding nationality: {ex.Message}");
                return ApiResponse<GetNationalityDTO>.Failure("Failed to add nationality");
            }
        }

        public async Task<ApiResponse<GetNationalityDTO>> UpdateNationalityAsync(UpdateNationalityDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.Nationality.UpdateNationality, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetNationalityDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Nationality updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating nationality: {ex.Message}");
                return ApiResponse<GetNationalityDTO>.Failure("Failed to update nationality");
            }
        }

        public async Task<ApiResponse<string>> DeleteNationalityAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.Nationality.DeleteNationality}?id={id}");
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Nationality deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting nationality: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete nationality");
            }
        }
    }

    // DTO Classes
    public class GetNationalityDTO
    {
        public int Id { get; set; }
        public string NationalityName { get; set; }
        public string NationalityNameAr { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddNationalityDTO
    {
        public string NationalityName { get; set; }
        public string NationalityNameAr { get; set; }
    }

    public class UpdateNationalityDTO
    {
        public int Id { get; set; }
        public string NationalityName { get; set; }
        public string NationalityNameAr { get; set; }
        public bool IsActive { get; set; }
    }
}
