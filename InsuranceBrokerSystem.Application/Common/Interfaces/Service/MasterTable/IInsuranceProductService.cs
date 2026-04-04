
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceProductService
    {
        public Task<Result<List<GetInsuranceProductDTO>>> GetInsuranceProductByInsuranceIdAsync(int id);
    }
}
