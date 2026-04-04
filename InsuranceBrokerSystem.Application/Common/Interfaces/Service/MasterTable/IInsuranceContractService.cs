
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceContractService
    {
        public Task<List<GetInsuranceContractDTO>> GetInsuranceContactByInsuranceIdAsync(int Id);
    }
}
