
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial
{
    public interface IInsuranceCompanyAccountService
    {
        Task<Result<bool>> GenerateAccountsAsync(int companyId);
    }
}
