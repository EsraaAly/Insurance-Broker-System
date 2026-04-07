namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceProductsByCompanyId
{
    public class GetInsuranceProductsByCompanyIdQuery : IRequest<Result<List<GetInsuranceProductDTO>>>
    {
        public int CompanyId { get; set; }
    }

    public class GetInsuranceProductsByCompanyIdHandler : IRequestHandler<GetInsuranceProductsByCompanyIdQuery, Result<List<GetInsuranceProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceProductsByCompanyIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceProductDTO>>> Handle(GetInsuranceProductsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.InsuranceProductRepository.GetInsuranceProductsByInsuranceIdAsync(request.CompanyId);
            if (products == null)
            {
                return Result<List<GetInsuranceProductDTO>>.Failure("No products found for this insurance company");
            }
            var dto = products.Adapt<List<GetInsuranceProductDTO>>();
            return Result<List<GetInsuranceProductDTO>>.Success(dto);
        }
    }
}
