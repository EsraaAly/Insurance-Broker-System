
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceProductRepository:IGenericRepository<InsuranceProduct>
    {
        public Task<List<InsuranceProduct>> GetInsuranceProductsByInsuranceIdAsync(int id);

    }
}
