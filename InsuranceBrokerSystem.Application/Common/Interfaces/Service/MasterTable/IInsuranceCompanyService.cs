
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceCompanyService
    {
        public Task<List<GetInsuranceCompanyDTO>> GetAllInsuranceCompaniesAsync();
              
        public Task<GetInsuranceCompanyDTO> AddInsuranceCompaniesAsync(AddInsuranceCompanyDTO Entity);
              
        public Task<GetInsuranceCompanyDTO> UpdateInsuranceCompaniesAsync(UpdateInsuranceCompanyDTO Entity);
              
        public Task<bool> DeleteInsuranceCompaniesAsync(int Id);
              
        public Task<GetInsuranceCompanyDTO> GetInsuranceCompaniesByIdAsync(int Id);
              
        public Task<GetInsuranceCompanyDTO> GetInsuranceCompaniesByNameAsync(string CompanyName);

        public Task<bool> RejectInsuranceCompaniesAsync(int id);
        public Task<bool> ApproveInsuranceCompaniesAsync(int id);
    }
}
