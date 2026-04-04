
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IRepoInsuranceProduct:IGenericRepository<InsuranceProduct>
    {
        public Task<List<InsuranceProduct>> GetInsuranceProductsByInsuranceIdAsync(int id);

    }
}
