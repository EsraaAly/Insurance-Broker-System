using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class SourceOfIncomeApiService
    {
        private readonly HttpClientService _httpClientService;

        public SourceOfIncomeApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetSourceOfIncomeDTO>>> GetAllSourceOfIncomesAsync()
        {
            return await _httpClientService.GetAsync<List<GetSourceOfIncomeDTO>>(ApiRoutes.MasterTable.SourceOfIncome.GetAllSourceOfIncomes);
        }

        public async Task<ApiResponse<GetSourceOfIncomeDTO>> GetSourceOfIncomeByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetSourceOfIncomeDTO>($"{ApiRoutes.MasterTable.SourceOfIncome.GetSourceOfIncomeById}?id={id}");
        }

        public async Task<ApiResponse<GetSourceOfIncomeDTO>> AddSourceOfIncomeAsync(AddSourceOfIncomeDTO dto)
        {
            return await _httpClientService.PostAsync<GetSourceOfIncomeDTO, AddSourceOfIncomeDTO>(ApiRoutes.MasterTable.SourceOfIncome.AddSourceOfIncome, dto);
        }

        public async Task<ApiResponse<GetSourceOfIncomeDTO>> UpdateSourceOfIncomeAsync(UpdateSourceOfIncomeDTO dto)
        {
            return await _httpClientService.PutAsync<GetSourceOfIncomeDTO, UpdateSourceOfIncomeDTO>(ApiRoutes.MasterTable.SourceOfIncome.UpdateSourceOfIncome, dto);
        }

        public async Task<ApiResponse<string>> DeleteSourceOfIncomeAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.SourceOfIncome.DeleteSourceOfIncome}?id={id}");
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
