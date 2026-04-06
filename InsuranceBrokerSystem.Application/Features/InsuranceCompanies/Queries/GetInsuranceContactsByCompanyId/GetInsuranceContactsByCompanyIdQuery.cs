namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceContactsByCompanyId
{
    public class GetInsuranceContactsByCompanyIdQuery : IRequest<Result<List<GetInsuranceContractDTO>>>
    {
        public int CompanyId { get; set; }
    }

    public class GetInsuranceContactsByCompanyIdHandler : IRequestHandler<GetInsuranceContactsByCompanyIdQuery, Result<List<GetInsuranceContractDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInsuranceContactsByCompanyIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetInsuranceContractDTO>>> Handle(GetInsuranceContactsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _unitOfWork.InsuranceContractRepository.GetInsuranceContactsByInsuranceIdAsync(request.CompanyId);
            if (contacts == null)
            {
                return Result<List<GetInsuranceContractDTO>>.Failure("No contacts found for this insurance company");
            }
            var dto = _mapper.Map<List<GetInsuranceContractDTO>>(contacts);
            return Result<List<GetInsuranceContractDTO>>.Success(dto);
        }
    }
}
