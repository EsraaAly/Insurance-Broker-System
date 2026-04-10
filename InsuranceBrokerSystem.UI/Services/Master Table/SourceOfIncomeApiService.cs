using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class SourceOfIncomeApiService
    {
        private readonly HttpClient _httpClient;

        public SourceOfIncomeApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<ApiResponse<List<GetSourceOfIncomeDTO>>> GetAllSourceOfIncomesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.SourceOfIncome.GetAllSourceOfIncomes);
                return await ApiResponseHandler.HandleResponseAsync<List<GetSourceOfIncomeDTO>>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving source of incomes: {ex.Message}");
                return ApiResponse<List<GetSourceOfIncomeDTO>>.Failure("Failed to retrieve source of incomes");
            }
        }

        public async Task<ApiResponse<GetSourceOfIncomeDTO>> GetSourceOfIncomeByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.SourceOfIncome.GetSourceOfIncomeById}?id={id}");
                return await ApiResponseHandler.HandleResponseAsync<GetSourceOfIncomeDTO>(response);
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error retrieving source of income: {ex.Message}");
                return ApiResponse<GetSourceOfIncomeDTO>.Failure("Failed to retrieve source of income");
            }
        }

        public async Task<ApiResponse<GetSourceOfIncomeDTO>> AddSourceOfIncomeAsync(AddSourceOfIncomeDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PostAsync(ApiRoutes.MasterTable.SourceOfIncome.AddSourceOfIncome, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetSourceOfIncomeDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Source of income added successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error adding source of income: {ex.Message}");
                return ApiResponse<GetSourceOfIncomeDTO>.Failure("Failed to add source of income");
            }
        }

        public async Task<ApiResponse<GetSourceOfIncomeDTO>> UpdateSourceOfIncomeAsync(UpdateSourceOfIncomeDTO dto)
        {
            try
            {
                var content = JsonContent.Create(dto);
                var response = await _httpClient.PutAsync(ApiRoutes.MasterTable.SourceOfIncome.UpdateSourceOfIncome, content);
                var result = await ApiResponseHandler.HandleResponseAsync<GetSourceOfIncomeDTO>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Source of income updated successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error updating source of income: {ex.Message}");
                return ApiResponse<GetSourceOfIncomeDTO>.Failure("Failed to update source of income");
            }
        }

        public async Task<ApiResponse<string>> DeleteSourceOfIncomeAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.SourceOfIncome.DeleteSourceOfIncome}?id={id}");
                var result = await ApiResponseHandler.HandleResponseAsync<string>(response);
                
                if (result.Successed)
                {
                    ApiResponseHandler.ShowSuccess("Source of income deleted successfully!");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error deleting source of income: {ex.Message}");
                return ApiResponse<string>.Failure("Failed to delete source of income");
            }
        }
    }

    // DTO Classes
    public class GetSourceOfIncomeDTO
    {
        public int Id { get; set; }
        public string SourceName { get; set; }
        public string SourceNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddSourceOfIncomeDTO
    {
        public string SourceName { get; set; }
        public string SourceNameAr { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSourceOfIncomeDTO
    {
        public int Id { get; set; }
        public string SourceName { get; set; }
        public string SourceNameAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
