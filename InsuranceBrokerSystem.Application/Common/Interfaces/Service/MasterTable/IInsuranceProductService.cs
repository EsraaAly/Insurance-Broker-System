
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceProductService
    {
        public Task<List<GetInsuranceProductDTO>> GetInsuranceProductByInsuranceIdAsync(int Id);

    }
}
