
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceProductService : IInsuranceProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<GetInsuranceProductDTO>>> GetInsuranceProductByInsuranceIdAsync(int id)
        {
            var products = await _unitOfWork.InsuranceProductRepository.GetInsuranceProductsByInsuranceIdAsync(id);
            if (products == null)
            {
                return Result<List<GetInsuranceProductDTO>>.Failure("No products found for this insurance company");
            }
            var dto = products.Adapt<List<GetInsuranceProductDTO>>();
            return Result<List<GetInsuranceProductDTO>>.Success(dto);
        }
    }
}
