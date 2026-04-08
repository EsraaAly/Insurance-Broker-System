using InsuranceBrokerSystem.Application.DTOs.Master_Table;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceClassApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44314";

        public InsuranceClassApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ApiResponse<List<GetInsuranceClassDTO>>> GetAllClassesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.InsuranceClass.GetAllInsuranceClasses);
                return await ApiResponseHandler.HandleResponseAsync<List<GetInsuranceClassDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving insurance classes: {ex.Message}");
                return ApiResponse<List<GetInsuranceClassDTO>>.Failure("Failed to retrieve insurance classes");
            }
        }

        public async Task<ApiResponse<GetInsuranceClassDTO>> AddClassAsync(AddInsuranceClassDTO newClass)
        {
            try
            {
                var content = JsonContent.Create(newClass);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.InsuranceClass.AddInsuranceClass, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceClassDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance class added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding insurance class: {ex.Message}");
                return ApiResponse<GetInsuranceClassDTO>.Failure("Failed to add insurance class");
            }
        }

        public async Task<ApiResponse<GetInsuranceClassDTO>> UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.InsuranceClass.UpdateInsuranceClass, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceClassDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance class updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating insurance class: {ex.Message}");
                return ApiResponse<GetInsuranceClassDTO>.Failure("Failed to update insurance class");
            }
        }

        public async Task<ApiResponse<string>> DeleteClassAsync(int id)
        {
            try
            {
                var url = ApiRoutes.MasterTable.InsuranceClass.DeleteInsuranceClass.Replace("{id}", id.ToString());
                var response = await _httpClient.DeleteAsync(url);
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance class deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting insurance class: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete insurance class");
            }
        }
    }
}
