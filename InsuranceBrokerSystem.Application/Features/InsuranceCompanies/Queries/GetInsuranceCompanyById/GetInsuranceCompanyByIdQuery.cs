namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceCompanyById
{
    public class GetInsuranceCompanyByIdQuery : IRequest<Result<GetInsuranceCompanyDTO>>
    {
        public int Id { get; set; }
    }

    public class GetInsuranceCompanyByIdHandler : IRequestHandler<GetInsuranceCompanyByIdQuery, Result<GetInsuranceCompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceCompanyByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> Handle(GetInsuranceCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var entry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(request.Id);
            if (entry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            GetInsuranceCompanyDTO dto = entry.Adapt<GetInsuranceCompanyDTO>();
            return Result<GetInsuranceCompanyDTO>.Success(dto);
        }
    }
}
