
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceProductService : IInsuranceProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InsuranceProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetInsuranceProductDTO>> GetInsuranceProductByInsuranceIdAsync(int Id)
        {
            List<GetInsuranceProductDTO> dto = _mapper.Map<List<GetInsuranceProductDTO>>(await _unitOfWork.InsuranceProduct.GetInsuranceProductsByInsuranceIdAsync(Id));
            return dto;
        }
    }
}
