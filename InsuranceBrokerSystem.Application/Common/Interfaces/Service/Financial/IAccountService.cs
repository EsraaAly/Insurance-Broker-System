
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial
{
    public interface IAccountService
    {
        Task<GetAccountDTO> AddAccountAsync(CreateAccountDTO dto);
        Task<GetAccountDTO> UpdateAccountAsync(EditAccountDTO dto);
        Task<List<GetAccountDTO>> GetRootAccountsAsync();
        Task<GetAccountDTO> GetAccountByIdAsync(int id);
        Task<List<GetAccountDTO>> GetAllAccountsAsync();
        Task<bool> DeleteAccountAsync(int id);
    }
}
