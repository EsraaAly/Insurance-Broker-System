namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.AddInsuranceCompany
{
    public class AddInsuranceCompanyCommand : IRequest<Result<GetInsuranceCompanyDTO>>
    {
        public string CompanyName { get; set; }
        public string CompanyNameAr { get; set; }
        public string VATNo { get; set; }
        public string Tele { get; set; }
        public string Abbreviation { get; set; }
        public string CRNo { get; set; }
        public string Email { get; set; }
        public string UnifiedNo { get; set; }
        public string BuildingNo { get; set; }
        public string AdditionalNo { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNameArabic { get; set; }
        public string StreetName { get; set; }
        public string StreetNameArabic { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameArabic { get; set; }
        public string PostalZIPCode { get; set; }
        public string CityName { get; set; }
        public string CityNameArabic { get; set; }
        public string State { get; set; }
        public string StateArabic { get; set; }
        public string CountryRegion { get; set; }
        public string CountryRegionArabic { get; set; }

        public string? AccNoCommAccrued { get; set; }
        public string? AccNoCommDue { get; set; }
        public string? AccNoVATAccrued { get; set; }
        public string? AccNoVATReceivable { get; set; }
        public string? AccNoGrossPremium { get; set; }
        public string? AccNoGrossVAT { get; set; }
        public string? AccNoNetPremium { get; set; }
        public string? AccNoUWVATPayable { get; set; }

        public List<AddInsuranceProductDTO> Products { get; set; } = new List<AddInsuranceProductDTO>();
        public List<AddInsuranceContractDTO> Contacts { get; set; } = new List<AddInsuranceContractDTO>();
    }

    public class AddInsuranceCompanyHandler : IRequestHandler<AddInsuranceCompanyCommand, Result<GetInsuranceCompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddInsuranceCompanyHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> Handle(AddInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            InsuranceCompany entry = _mapper.Map<InsuranceCompany>(request);
            entry.CreatedBy = "Israa";
            entry.CreatedDate = DateTime.Now;

            foreach (var e in entry.Contacts)
            {
                e.CreatedBy = "Israa";
                e.CreatedDate = DateTime.Now;
                // Note: the association is handled by EF if mapped correctly, 
                // but we might need to set the parent ID if it's not set automatically by navigation.
                // However, since 'entry' is new, its ID will be generated upon save.
            }
            foreach (var e in entry.Products)
            {
                e.CreatedBy = "Israa";
                e.CreatedDate = DateTime.Now;
            }

            entry = await _unitOfWork.InsuranceCompanyRepository.AddEntityAsync(entry);
            await _unitOfWork.CommitAsync();

            GetInsuranceCompanyDTO companyRes = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return Result<GetInsuranceCompanyDTO>.Success(companyRes, "Insurance Company added successfully");
        }
    }
}
