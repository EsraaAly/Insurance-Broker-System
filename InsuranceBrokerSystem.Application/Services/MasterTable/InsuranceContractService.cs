
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceContractService : IInsuranceContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceContractDTO>>> GetInsuranceContactByInsuranceIdAsync(int id)
        {
            var contacts = await _unitOfWork.InsuranceContractRepository.GetInsuranceContactsByInsuranceIdAsync(id);
            if (contacts == null)
            {
                return Result<List<GetInsuranceContractDTO>>.Failure("No contacts found for this insurance company");
            }
            var dto = contacts.Adapt<List<GetInsuranceContractDTO>>();
            return Result<List<GetInsuranceContractDTO>>.Success(dto);
        }
    }
}
