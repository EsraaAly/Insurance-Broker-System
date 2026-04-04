
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceContractService
    {
        public Task<Result<List<GetInsuranceContractDTO>>> GetInsuranceContactByInsuranceIdAsync(int id);
    }
}
