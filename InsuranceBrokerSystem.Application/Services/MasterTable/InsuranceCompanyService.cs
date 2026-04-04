
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        private readonly IInsuranceCompanyAccountService _insuranceCompanyAccountService;
        private readonly IMapper _mapper;
        
        public InsuranceCompanyService(IUnitOfWork unitOfWork,
                                       IMapper mapper,IAccountService accountService,
                                       IInsuranceCompanyAccountService insuranceCompanyAccountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountService = accountService;
            _insuranceCompanyAccountService = insuranceCompanyAccountService;
        }

        public async Task<GetInsuranceCompanyDTO> AddInsuranceCompaniesAsync(AddInsuranceCompanyDTO dto)
        {

            InsuranceCompany entry = _mapper.Map<InsuranceCompany>(dto);

            entry.CreatedBy = "Israa";
            entry.CreatedDate = DateTime.Now;

            foreach(var e in entry.Contacts)
            {
                e.CreatedBy = "Israa";
                e.CreatedDate = DateTime.Now;
                e.InsuranceCompanyId = entry.Id;
                //await _unitOfWork.G_repoInsuranceContract.AddEntityAsync(e);
            }
            foreach (var e in entry.Products)
            {
                e.CreatedBy = "Israa";
                e.CreatedDate = DateTime.Now;
                e.InsuranceCompanyId = entry.Id;
                //await _unitOfWork.G_repoInsuranceProduct.AddEntityAsync(e);
            }

            entry = await _unitOfWork.InsuranceCompany.AddEntityAsync(entry);

            //await _unitOfWork._G_repoInsuranceContract.AddEntityAsync(entry);
            await _unitOfWork.CommitAsync();
            GetInsuranceCompanyDTO company = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return company;
        }

        public async Task<bool> DeleteInsuranceCompaniesAsync(int Id)
        {
           var success =  await _unitOfWork.InsuranceCompany.DeleteEntityAsync(Id);
            
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<List<GetInsuranceCompanyDTO>> GetAllInsuranceCompaniesAsync()
        {
             var entries = await _unitOfWork.InsuranceCompany.GetAllEntitytiesAsync();
            List<GetInsuranceCompanyDTO> dto = _mapper.Map<List<GetInsuranceCompanyDTO>>(entries);
            //List<GetInsuranceCompanyDTO> dto = entries.Select(e => new GetInsuranceCompanyDTO
            //{
            //    Id = e.Id,
            //    CompanyName=e.CompanyName,
            //    CompanyNameAr=e.CompanyNameAr,
            //    Abbreviation=e.Abbreviation,
            //    AdditionalNo=e.AdditionalNo,
            //    BuildingName=e.BuildingName,
            //    BuildingNameArabic=e.BuildingNameArabic,
            //    BuildingNo=e.BuildingNo,
            //    CityName=e.CityName,
            //    CityNameArabic=e.CityNameArabic,
            //    CountryRegion=e.CountryRegion,
            //    CountryRegionArabic=e.CountryRegionArabic,
            //    CreatedBy=e.CreatedBy,
            //    CreatedDate=e.CreatedDate,
            //    CRNo=e.CRNo,
            //    DistrictName=e.DistrictName,
            //    DistrictNameArabic=e.DistrictNameArabic,
            //    Email=e.Email,
            //    PostalZIPCode=e.PostalZIPCode,
            //    State=e.State,
            //    StateArabic=e.StateArabic,
            //    StreetName=e.StreetName,
            //    StreetNameArabic=e.StreetNameArabic,
            //    Tele=e.Tele,
            //    UnifiedNo=e.UnifiedNo,
            //    UpdatedBy=e.UpdatedBy,
            //    UpdatedDate=e.UpdatedDate,
            //    VATNo=e.VATNo                
            //}).ToList();

            return dto;
        }

        public async Task<GetInsuranceCompanyDTO> GetInsuranceCompaniesByIdAsync(int Id)
        {
            var entry = await _unitOfWork.InsuranceCompany.GetEntityByIdAsync(Id);

            GetInsuranceCompanyDTO dto =  _mapper.Map< GetInsuranceCompanyDTO>(entry);

            //GetInsuranceCompanyDTO dto = new GetInsuranceCompanyDTO
            //{
            //    Id = entry.Id,
            //    CompanyName = entry.CompanyName,
            //    CompanyNameAr = entry.CompanyNameAr,
            //    Abbreviation = entry.Abbreviation,
            //    AdditionalNo = entry.AdditionalNo,
            //    BuildingName = entry.BuildingName,
            //    BuildingNameArabic = entry.BuildingNameArabic,
            //    BuildingNo = entry.BuildingNo,
            //    CityName = entry.CityName,
            //    CityNameArabic = entry.CityNameArabic,
            //    CountryRegion = entry.CountryRegion,
            //    CountryRegionArabic = entry.CountryRegionArabic,
            //    CreatedBy = entry.CreatedBy,
            //    CreatedDate = entry.CreatedDate,
            //    CRNo = entry.CRNo,
            //    DistrictName = entry.DistrictName,
            //    DistrictNameArabic = entry.DistrictNameArabic,
            //    Email = entry.Email,
            //    PostalZIPCode = entry.PostalZIPCode,
            //    State = entry.State,
            //    StateArabic = entry.StateArabic,
            //    StreetName = entry.StreetName,
            //    StreetNameArabic = entry.StreetNameArabic,
            //    Tele = entry.Tele,
            //    UnifiedNo = entry.UnifiedNo,
            //    UpdatedBy = entry.UpdatedBy,
            //    UpdatedDate = entry.UpdatedDate,
            //    VATNo = entry.VATNo
            //};

            return dto;
        }

        public async Task<GetInsuranceCompanyDTO> GetInsuranceCompaniesByNameAsync(string CompanyName)
        {
            var entry = await _unitOfWork.InsuranceCompany.GetInsuranceCompaniesByNameAsync(CompanyName);

            GetInsuranceCompanyDTO dto = _mapper.Map<GetInsuranceCompanyDTO>(entry);

            return dto;
        }

        public async Task<GetInsuranceCompanyDTO> UpdateInsuranceCompaniesAsync(UpdateInsuranceCompanyDTO dto)
        {
            var existingEntry = await _unitOfWork.InsuranceCompany.GetEntityByIdWithIncludesAsync(dto.Id,x=>x.Products,x=>x.Contacts);

            if (existingEntry == null)
            {
                return null;
            }

            _mapper.Map(dto,existingEntry);

            existingEntry.UpdatedBy = "Israa";
            existingEntry.UpdatedDate = DateTime.Now;

            var success = await _unitOfWork.InsuranceCompany.UpdateEntityAsync(existingEntry);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return _mapper.Map<GetInsuranceCompanyDTO>(existingEntry);
            }
            return null;
        }

        public async Task<bool> ApproveInsuranceCompaniesAsync(int id)
        {
            
            var existingEntry = await _unitOfWork.InsuranceCompany.GetEntityByIdAsync(id);

            if (existingEntry == null)
            {
                return false;
            }

            //CreateAccountDTO dTO = new CreateAccountDTO
            //{
            //    AccountName = existingEntry.CompanyName,
            //    AccountType = AccountType.Asset,
            //    ParentId = null,
            //    Level = 1
            //};
            //var CommAccrued = await _accountService.AddAccountAsync();

            existingEntry.IsApproved = true;
            existingEntry.IsRejected = false;
            existingEntry.ApprovedBy = "Israa";
            existingEntry.ApprovedDate = DateTime.Now;

            //existingEntry.AccNoCommAccrued = CommAccrued.AccountNumber;//liability 002-001-001-000
            //existingEntry.AccNoCommDue = account.AccountNumber;//liability 002-001-001-000
            //existingEntry.AccNoVATAccrued = account.AccountNumber;//liability 002-001-002-000
            //existingEntry.AccNoVATReceivable = account.AccountNumber;//assets 001-001-002-000
            //existingEntry.AccNoGrossPremium = account.AccountNumber;//revenue 004-001-001-000
            //existingEntry.AccNoGrossVAT = account.AccountNumber;//revenue 
            //existingEntry.AccNoNetPremium = account.AccountNumber;//revenue 004-001-001-000
            //existingEntry.AccNoUWVATPayable = account.AccountNumber;//liability 002-001-002-000


            var success = await _unitOfWork.InsuranceCompany.UpdateEntityAsync(existingEntry);
            if (success)
            {
               
                await _insuranceCompanyAccountService.GenerateAccountsAsync(existingEntry.Id);
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectInsuranceCompaniesAsync(int id)
        {
            return await _unitOfWork.InsuranceCompany.RejectInsuranceCompaniesAsync(id);
        }
    }
}
