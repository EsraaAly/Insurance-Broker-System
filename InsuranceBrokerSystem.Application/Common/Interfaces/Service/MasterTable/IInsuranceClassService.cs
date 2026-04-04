
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceClassService
    {
        public Task<List<GetInsuranceClassDTO>> GetAllClassesAsync();
        public Task<GetInsuranceClassDTO> GetClassByidAsync(int id);
        public Task<bool> AddClassAsync(AddInsuranceClassDTO insuranceClass);
        public Task<GetInsuranceClassDTO> UpdateClassAsync(UpdateInsuranceClassDTO insuranceClass);
        public Task<bool> DeleteClassAsync(int id);
    }
}
