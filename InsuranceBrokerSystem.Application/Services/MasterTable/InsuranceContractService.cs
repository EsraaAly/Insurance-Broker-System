
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

        public async Task<List<GetInsuranceContractDTO>> GetInsuranceContactByInsuranceIdAsync(int Id)
        {
            List<GetInsuranceContractDTO> dto = _mapper.Map<List<GetInsuranceContractDTO>>(await _unitOfWork.InsuranceContract.GetInsuranceContactsByInsuranceIdAsync(Id));
            return dto;
        }
    }
}
