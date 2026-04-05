
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceContactRepository : IGenericRepository<InsuranceContact>
    {
        public Task<List<InsuranceContact>> GetInsuranceContactsByInsuranceIdAsync(int id);

    }
}
