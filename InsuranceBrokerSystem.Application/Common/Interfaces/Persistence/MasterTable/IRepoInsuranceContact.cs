
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IRepoInsuranceContact : IGenericRepository<InsuranceContact>
    {
        public Task<List<InsuranceContact>> GetInsuranceContactsByInsuranceIdAsync(int id);

    }
}
