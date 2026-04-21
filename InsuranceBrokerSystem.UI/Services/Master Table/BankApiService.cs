using InsuranceBrokerSystem.UI;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class BankApiService
    {
        private readonly HttpClientService _httpClientService;

        public BankApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetBankDTO>>> GetAllBanksAsync()
        {
            return await _httpClientService.GetAsync<List<GetBankDTO>>(ApiRoutes.MasterTable.Bank.GetAllBanks);
        }

        public async Task<ApiResponse<GetBankDTO>> GetBankByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetBankDTO>($"{ApiRoutes.MasterTable.Bank.GetBankById}?id={id}");
        }

        public async Task<ApiResponse<GetBankDTO>> AddBankAsync(AddBankDTO dto)
        {
            return await _httpClientService.PostAsync<GetBankDTO, AddBankDTO>(ApiRoutes.MasterTable.Bank.AddBank, dto);
        }

        public async Task<ApiResponse<GetBankDTO>> UpdateBankAsync(UpdateBankDTO dto)
        {
            return await _httpClientService.PutAsync<GetBankDTO, UpdateBankDTO>(ApiRoutes.MasterTable.Bank.UpdateBank, dto);
        }

        public async Task<ApiResponse<string>> DeleteBankAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.Bank.DeleteBank}?id={id}");
        }
    }
}
