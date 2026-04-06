namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceCompanyByName
{
    public class GetInsuranceCompanyByNameQuery : IRequest<Result<GetInsuranceCompanyDTO>>
    {
        public string Name { get; set; }
    }

    public class GetInsuranceCompanyByNameHandler : IRequestHandler<GetInsuranceCompanyByNameQuery, Result<GetInsuranceCompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInsuranceCompanyByNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> Handle(GetInsuranceCompanyByNameQuery request, CancellationToken cancellationToken)
        {
            var entry = await _unitOfWork.InsuranceCompanyRepository.GetInsuranceCompaniesByNameAsync(request.Name);
            if (entry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            GetInsuranceCompanyDTO dto = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return Result<GetInsuranceCompanyDTO>.Success(dto);
        }
    }
}
