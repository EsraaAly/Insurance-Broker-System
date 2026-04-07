namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceCompanyByName
{
    public class GetInsuranceCompanyByNameQuery : IRequest<Result<GetInsuranceCompanyDTO>>
    {
        public string Name { get; set; }
    }

    public class GetInsuranceCompanyByNameHandler : IRequestHandler<GetInsuranceCompanyByNameQuery, Result<GetInsuranceCompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceCompanyByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> Handle(GetInsuranceCompanyByNameQuery request, CancellationToken cancellationToken)
        {
            var entry = await _unitOfWork.InsuranceCompanyRepository.GetInsuranceCompaniesByNameAsync(request.Name);
            if (entry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            GetInsuranceCompanyDTO dto = entry.Adapt<GetInsuranceCompanyDTO>();
            return Result<GetInsuranceCompanyDTO>.Success(dto);
        }
    }
}
