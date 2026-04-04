
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial
{
    public interface IInsuranceCompanyAccountService
    {
        Task<bool> GenerateAccountsAsync(int companyId);
    }
}
