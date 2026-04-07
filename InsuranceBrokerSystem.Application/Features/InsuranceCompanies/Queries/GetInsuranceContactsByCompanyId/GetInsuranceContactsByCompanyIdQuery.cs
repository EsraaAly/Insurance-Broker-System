namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceContactsByCompanyId
{
    public class GetInsuranceContactsByCompanyIdQuery : IRequest<Result<List<GetInsuranceContractDTO>>>
    {
        public int CompanyId { get; set; }
    }

    public class GetInsuranceContactsByCompanyIdHandler : IRequestHandler<GetInsuranceContactsByCompanyIdQuery, Result<List<GetInsuranceContractDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceContactsByCompanyIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceContractDTO>>> Handle(GetInsuranceContactsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _unitOfWork.InsuranceContractRepository.GetInsuranceContactsByInsuranceIdAsync(request.CompanyId);
            if (contacts == null)
            {
                return Result<List<GetInsuranceContractDTO>>.Failure("No contacts found for this insurance company");
            }
            var dto = contacts.Adapt<List<GetInsuranceContractDTO>>();
            return Result<List<GetInsuranceContractDTO>>.Success(dto);
        }
    }
}
