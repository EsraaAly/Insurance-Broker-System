namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceCompanyById
{
    public class GetInsuranceCompanyByIdQuery : IRequest<Result<GetInsuranceCompanyDTO>>
    {
        public int Id { get; set; }
    }

    public class GetInsuranceCompanyByIdHandler : IRequestHandler<GetInsuranceCompanyByIdQuery, Result<GetInsuranceCompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInsuranceCompanyByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> Handle(GetInsuranceCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var entry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(request.Id);
            if (entry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            GetInsuranceCompanyDTO dto = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return Result<GetInsuranceCompanyDTO>.Success(dto);
        }
    }
}
