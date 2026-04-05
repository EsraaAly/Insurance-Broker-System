
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceLOBRepository : IGenericRepository<InsuranceLineOfBusiness>
    {
        public Task<List<InsuranceLineOfBusiness>> GetInsuranceLOBByClassIdAsync(int ClassId);
    }
}
