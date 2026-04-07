namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetAllInsuranceCompanies
{
    public class GetAllInsuranceCompaniesQuery : IRequest<Result<List<GetInsuranceCompanyDTO>>>
    {
    }

    public class GetAllInsuranceCompaniesHandler : IRequestHandler<GetAllInsuranceCompaniesQuery, Result<List<GetInsuranceCompanyDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllInsuranceCompaniesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceCompanyDTO>>> Handle(GetAllInsuranceCompaniesQuery request, CancellationToken cancellationToken)
        {
            var entries = await _unitOfWork.InsuranceCompanyRepository.GetAllEntitytiesAsync();
            if (entries == null)
            {
                return Result<List<GetInsuranceCompanyDTO>>.Failure("No insurance companies found");
            }
            List<GetInsuranceCompanyDTO> dto = entries.Adapt<List<GetInsuranceCompanyDTO>>();
            return Result<List<GetInsuranceCompanyDTO>>.Success(dto);
        }
    }
}
