namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceProductsByCompanyId
{
    public class GetInsuranceProductsByCompanyIdQuery : IRequest<Result<List<GetInsuranceProductDTO>>>
    {
        public int CompanyId { get; set; }
    }

    public class GetInsuranceProductsByCompanyIdHandler : IRequestHandler<GetInsuranceProductsByCompanyIdQuery, Result<List<GetInsuranceProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInsuranceProductsByCompanyIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetInsuranceProductDTO>>> Handle(GetInsuranceProductsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.InsuranceProductRepository.GetInsuranceProductsByInsuranceIdAsync(request.CompanyId);
            if (products == null)
            {
                return Result<List<GetInsuranceProductDTO>>.Failure("No products found for this insurance company");
            }
            var dto = _mapper.Map<List<GetInsuranceProductDTO>>(products);
            return Result<List<GetInsuranceProductDTO>>.Success(dto);
        }
    }
}
