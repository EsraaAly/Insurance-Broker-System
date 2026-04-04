
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceClassService
    {
        public Task<Result<List<GetInsuranceClassDTO>>> GetAllClassesAsync();
        public Task<Result<GetInsuranceClassDTO>> GetClassByidAsync(int id);
        public Task<Result<bool>> AddClassAsync(AddInsuranceClassDTO insuranceClass);
        public Task<Result<GetInsuranceClassDTO>> UpdateClassAsync(UpdateInsuranceClassDTO insuranceClass);
        public Task<Result<bool>> DeleteClassAsync(int id);
    }
}
