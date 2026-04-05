
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceContractService : IInsuranceContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InsuranceContractService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetInsuranceContractDTO>>> GetInsuranceContactByInsuranceIdAsync(int id)
        {
            var contacts = await _unitOfWork.InsuranceContractRepository.GetInsuranceContactsByInsuranceIdAsync(id);
            if (contacts == null)
            {
                return Result<List<GetInsuranceContractDTO>>.Failure("No contacts found for this insurance company");
            }
            var dto = _mapper.Map<List<GetInsuranceContractDTO>>(contacts);
            return Result<List<GetInsuranceContractDTO>>.Success(dto);
        }
    }
}
