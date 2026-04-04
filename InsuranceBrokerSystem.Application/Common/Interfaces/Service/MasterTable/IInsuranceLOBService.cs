
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceLOBService
    {
        public Task<Result<List<GetInsuranceLOBDTO>>> GetAllLOBsAsync();
        public Task<Result<GetInsuranceLOBDTO>> GetLOBByidAsync(int id);
        public Task<Result<List<GetInsuranceLOBDTO>>> GetInsuranceLOBByClassIdAsync(int classId);
        public Task<Result<bool>> AddLOBAsync(AddInsuranceLOBDTO insuranceLOB);
        public Task<Result<GetInsuranceLOBDTO>> UpdateLOBAsync(UpdateInsuranceLOBDTO insuranceLOB);
        public Task<Result<bool>> DeleteLOBAsync(int id);
    }
}
