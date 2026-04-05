
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
        public async Task<Result<List<GetInsuranceProductDTO>>> GetInsuranceProductByInsuranceIdAsync(int id)
        {
            var products = await _unitOfWork.InsuranceProductRepository.GetInsuranceProductsByInsuranceIdAsync(id);
            if (products == null)
            {
                return Result<List<GetInsuranceProductDTO>>.Failure("No products found for this insurance company");
            }
            var dto = _mapper.Map<List<GetInsuranceProductDTO>>(products);
            return Result<List<GetInsuranceProductDTO>>.Success(dto);
        }
    }
}
