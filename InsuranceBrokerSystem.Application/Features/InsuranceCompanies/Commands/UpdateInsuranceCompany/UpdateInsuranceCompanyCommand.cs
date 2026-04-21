namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.UpdateInsuranceCompany
{
    public class UpdateInsuranceCompanyCommand : IRequest<Result<GetInsuranceCompanyDTO>>
    {
        public UpdateInsuranceCompanyDTO _updateInsuranceCompanyDTO { get; set; }
        
        // Keep existing properties for backward compatibility
        public int Id { get; set; }
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

        public List<UpdateInsuranceProductDTO> Products { get; set; } = new List<UpdateInsuranceProductDTO>();
        public List<UpdateInsuranceContractDTO> Contacts { get; set; } = new List<UpdateInsuranceContractDTO>();
    }

    public class UpdateInsuranceCompanyHandler : IRequestHandler<UpdateInsuranceCompanyCommand, Result<GetInsuranceCompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInsuranceCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> Handle(UpdateInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingEntry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdWithIncludesAsync(request.Id, x => x.Products, x => x.Contacts);

            if (existingEntry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            request.Adapt(existingEntry);
            existingEntry.UpdatedBy = "SystemAudit";
            existingEntry.UpdatedDate = DateTime.Now;

            var success = await _unitOfWork.InsuranceCompanyRepository.UpdateEntityAsync(existingEntry);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetInsuranceCompanyDTO>.Success(existingEntry.Adapt<GetInsuranceCompanyDTO>(), "Insurance Company updated successfully");
            }
            return Result<GetInsuranceCompanyDTO>.Failure("Failed to update Insurance Company");
        }
    }
}
