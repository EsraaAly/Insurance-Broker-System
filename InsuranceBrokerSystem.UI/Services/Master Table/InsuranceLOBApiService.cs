using InsuranceBrokerSystem.Application.DTOs.Master_Table;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceLOBApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44314";

        public InsuranceLOBApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ApiResponse<List<GetInsuranceLOBDTO>>> GetAllLOBAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.InsuranceLOB.GetAllInsuranceLOBs);
                return await ApiResponseHandler.HandleResponseAsync<List<GetInsuranceLOBDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving insurance LOBs: {ex.Message}");
                return ApiResponse<List<GetInsuranceLOBDTO>>.Failure("Failed to retrieve insurance LOBs");
            }
        }

        public async Task<ApiResponse<List<GetInsuranceLOBDTO>>> GetLOBByClassIdAsync(int classId)
        {
            try
            {
                var url = ApiRoutes.MasterTable.InsuranceLOB.GetLOBByClassIdAsync.Replace("{id}", classId.ToString());
                var response = await _httpClient.GetAsync(url);
                return await ApiResponseHandler.HandleResponseAsync<List<GetInsuranceLOBDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving insurance LOBs: {ex.Message}");
                return ApiResponse<List<GetInsuranceLOBDTO>>.Failure("Failed to retrieve insurance LOBs");
            }
        }

        public async Task<ApiResponse<GetInsuranceLOBDTO>> AddLOBAsync(AddInsuranceLOBDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.InsuranceLOB.AddInsuranceLOB, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceLOBDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance LOB added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding insurance LOB: {ex.Message}");
                return ApiResponse<GetInsuranceLOBDTO>.Failure("Failed to add insurance LOB");
            }
        }

        public async Task<ApiResponse<string>> DeleteLOBAsync(int id)
        {
            try
            {
                var url = ApiRoutes.MasterTable.InsuranceLOB.DeleteInsuranceLOB.Replace("{id}", id.ToString());
                var response = await _httpClient.DeleteAsync(url);
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance LOB deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting insurance LOB: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete insurance LOB");
            }
        }

        public async Task<ApiResponse<GetInsuranceLOBDTO>> UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.InsuranceLOB.UpdateInsuranceLOB, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetInsuranceLOBDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Insurance LOB updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating insurance LOB: {ex.Message}");
                return ApiResponse<GetInsuranceLOBDTO>.Failure("Failed to update insurance LOB");
            }
        }
    }
}
