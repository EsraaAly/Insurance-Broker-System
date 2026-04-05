
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceCompanyRepository: IGenericRepository<InsuranceCompany>
    {
        //public Task<List<InsuranceCompany>> GetAllInsuranceCompaniesAsync();

        //public Task<InsuranceCompany> AddInsuranceCompaniesAsync(InsuranceCompany Entity);

        //public Task<bool> UpdateInsuranceCompaniesAsync(InsuranceCompany Entity);

        //public Task<bool> DeleteInsuranceCompaniesAsync(int Id);

        //public Task<InsuranceCompany> GetInsuranceCompaniesByIdAsync(int Id);

        public Task<InsuranceCompany> GetInsuranceCompaniesByNameAsync(string CompanyName);
        //public  Task<bool> ApproveInsuranceCompaniesAsync(InsuranceCompany insuranceCompany);
        public Task<bool> RejectInsuranceCompaniesAsync(int id);

    }
}
