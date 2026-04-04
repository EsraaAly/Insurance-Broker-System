
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial
{
    public interface IAccountService
    {
        Task<Result<GetAccountDTO>> AddAccountAsync(CreateAccountDTO dto);
        Task<Result<GetAccountDTO>> UpdateAccountAsync(EditAccountDTO dto);
        Task<Result<List<GetAccountDTO>>> GetRootAccountsAsync();
        Task<Result<GetAccountDTO>> GetAccountByIdAsync(int id);
        Task<Result<List<GetAccountDTO>>> GetAllAccountsAsync();
        Task<Result<bool>> DeleteAccountAsync(int id);
    }
}
