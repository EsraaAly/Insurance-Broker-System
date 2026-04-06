namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetAllInsuranceCompanies
{
    public class GetAllInsuranceCompaniesQuery : IRequest<Result<List<GetInsuranceCompanyDTO>>>
    {
    }

    public class GetAllInsuranceCompaniesHandler : IRequestHandler<GetAllInsuranceCompaniesQuery, Result<List<GetInsuranceCompanyDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllInsuranceCompaniesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetInsuranceCompanyDTO>>> Handle(GetAllInsuranceCompaniesQuery request, CancellationToken cancellationToken)
        {
            var entries = await _unitOfWork.InsuranceCompanyRepository.GetAllEntitytiesAsync();
            if (entries == null)
            {
                return Result<List<GetInsuranceCompanyDTO>>.Failure("No insurance companies found");
            }
            List<GetInsuranceCompanyDTO> dto = _mapper.Map<List<GetInsuranceCompanyDTO>>(entries);
            return Result<List<GetInsuranceCompanyDTO>>.Success(dto);
        }
    }
}
