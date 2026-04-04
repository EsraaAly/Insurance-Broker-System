
namespace InsuranceBrokerSystem.Application.Interfaces.Master_Table
{
    public interface IInsuranceLOBService
    {
        public Task<List<GetInsuranceLOBDTO>> GetAllLOBsAsync();
        public Task<GetInsuranceLOBDTO> GetLOBByidAsync(int id);
        public Task<List<GetInsuranceLOBDTO>> GetInsuranceLOBByClassIdAsync(int ClassId);
        public Task<bool> AddLOBAsync(AddInsuranceLOBDTO insuranceLOB);
        public Task<GetInsuranceLOBDTO> UpdateLOBAsync(UpdateInsuranceLOBDTO insuranceLOB);
        public Task<bool> DeleteLOBAsync(int id);
    }
}
