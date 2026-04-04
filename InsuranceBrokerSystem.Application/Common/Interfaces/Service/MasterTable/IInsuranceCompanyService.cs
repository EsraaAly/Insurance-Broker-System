
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceCompanyService
    {
        public Task<Result<List<GetInsuranceCompanyDTO>>> GetAllInsuranceCompaniesAsync();
        public Task<Result<GetInsuranceCompanyDTO>> AddInsuranceCompaniesAsync(AddInsuranceCompanyDTO entity);
        public Task<Result<GetInsuranceCompanyDTO>> UpdateInsuranceCompaniesAsync(UpdateInsuranceCompanyDTO entity);
        public Task<Result<bool>> DeleteInsuranceCompaniesAsync(int id);
        public Task<Result<GetInsuranceCompanyDTO>> GetInsuranceCompaniesByIdAsync(int id);
        public Task<Result<GetInsuranceCompanyDTO>> GetInsuranceCompaniesByNameAsync(string companyName);
        public Task<Result<bool>> RejectInsuranceCompaniesAsync(int id);
        public Task<Result<bool>> ApproveInsuranceCompaniesAsync(int id);
    }
}
