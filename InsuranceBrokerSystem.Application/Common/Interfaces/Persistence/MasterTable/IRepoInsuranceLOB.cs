
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IRepoInsuranceLOB : IGenericRepository<InsuranceLineOfBusiness>
    {
        public Task<List<InsuranceLineOfBusiness>> GetInsuranceLOBByClassIdAsync(int ClassId);
    }
}
