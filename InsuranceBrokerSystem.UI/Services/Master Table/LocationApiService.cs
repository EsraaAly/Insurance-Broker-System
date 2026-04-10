using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class LocationApiService
    {
        private readonly HttpClient _httpClient;

        public LocationApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<ApiResponse<List<GetLocationDTO>>> GetAllLocationsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.Location.GetAllLocations);
                return await ApiResponseHandler.HandleResponseAsync<List<GetLocationDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving locations: {ex.Message}");
                return ApiResponse<List<GetLocationDTO>>.Failure("Failed to retrieve locations");
            }
        }

        public async Task<ApiResponse<GetLocationDTO>> GetLocationByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.Location.GetLocationById}?id={id}");
                return await ApiResponseHandler.HandleResponseAsync<GetLocationDTO>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving location: {ex.Message}");
                return ApiResponse<GetLocationDTO>.Failure("Failed to retrieve location");
            }
        }

        public async Task<ApiResponse<GetLocationDTO>> AddLocationAsync(AddLocationDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.Location.AddLocation, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetLocationDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Location added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding location: {ex.Message}");
                return ApiResponse<GetLocationDTO>.Failure("Failed to add location");
            }
        }

        public async Task<ApiResponse<GetLocationDTO>> UpdateLocationAsync(UpdateLocationDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.Location.UpdateLocation, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetLocationDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Location updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating location: {ex.Message}");
                return ApiResponse<GetLocationDTO>.Failure("Failed to update location");
            }
        }

        public async Task<ApiResponse<string>> DeleteLocationAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.Location.DeleteLocation}?id={id}");
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Location deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting location: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete location");
            }
        }
    }

    // DTO Classes
    public class GetLocationDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string CityNameAr { get; set; }
        public string Country { get; set; }
        public string CountryAr { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddLocationDTO
    {
        public string CityName { get; set; }
        public string CityNameAr { get; set; }
        public string Country { get; set; }
        public string CountryAr { get; set; }
    }

    public class UpdateLocationDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string CityNameAr { get; set; }
        public string Country { get; set; }
        public string CountryAr { get; set; }
        public bool IsActive { get; set; }
    }
}
