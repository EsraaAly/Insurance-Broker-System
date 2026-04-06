
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInsuranceCompanyAccountService _insuranceCompanyAccountService;
        private readonly IMapper _mapper;
        
        public InsuranceCompanyService(IUnitOfWork unitOfWork,
                                       IMapper mapper,
                                       IInsuranceCompanyAccountService insuranceCompanyAccountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _insuranceCompanyAccountService = insuranceCompanyAccountService;
        }

        public async Task<Result<GetInsuranceCompanyDTO>> AddInsuranceCompaniesAsync(AddInsuranceCompanyDTO dto)
        {
            if (dto == null) return Result<GetInsuranceCompanyDTO>.Failure("Data is null");

            InsuranceCompany entry = _mapper.Map<InsuranceCompany>(dto);
            entry.CreatedBy = "Israa";
            entry.CreatedDate = DateTime.Now;

            foreach (var e in entry.Contacts)
            {
                e.CreatedBy = "Israa";
                e.CreatedDate = DateTime.Now;
                e.InsuranceCompanyId = entry.Id;
            }
            foreach (var e in entry.Products)
            {
                e.CreatedBy = "Israa";
                e.CreatedDate = DateTime.Now;
                e.InsuranceCompanyId = entry.Id;
            }

            entry = await _unitOfWork.InsuranceCompanyRepository.AddEntityAsync(entry);
            await _unitOfWork.CommitAsync();

            GetInsuranceCompanyDTO company = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return Result<GetInsuranceCompanyDTO>.Success(company, "Insurance Company added successfully");
        }

        public async Task<Result<bool>> DeleteInsuranceCompaniesAsync(int id)
        {
            var success = await _unitOfWork.InsuranceCompanyRepository.DeleteEntityAsync(id);

            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Company deleted successfully");
            }
            return Result<bool>.Failure("Failed to delete Insurance Company or it does not exist");
        }

        public async Task<Result<List<GetInsuranceCompanyDTO>>> GetAllInsuranceCompaniesAsync()
        {
            var entries = await _unitOfWork.InsuranceCompanyRepository.GetAllEntitytiesAsync();
            if (entries == null)
            {
                return Result<List<GetInsuranceCompanyDTO>>.Failure("No insurance companies found");
            }
            List<GetInsuranceCompanyDTO> dto = _mapper.Map<List<GetInsuranceCompanyDTO>>(entries);
            return Result<List<GetInsuranceCompanyDTO>>.Success(dto);
        }

        public async Task<Result<GetInsuranceCompanyDTO>> GetInsuranceCompaniesByIdAsync(int id)
        {
            var entry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(id);
            if (entry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            GetInsuranceCompanyDTO dto = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return Result<GetInsuranceCompanyDTO>.Success(dto);
        }

        public async Task<Result<GetInsuranceCompanyDTO>> GetInsuranceCompaniesByNameAsync(string companyName)
        {
            var entry = await _unitOfWork.InsuranceCompanyRepository.GetInsuranceCompaniesByNameAsync(companyName);
            if (entry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            GetInsuranceCompanyDTO dto = _mapper.Map<GetInsuranceCompanyDTO>(entry);
            return Result<GetInsuranceCompanyDTO>.Success(dto);
        }

        /// <summary>
        /// Updates an existing Insurance Company and its related entities using a transactional Unit of Work.
        /// Demonstrates the use of AutoMapper for complex object-graph updates.
        /// </summary>
        public async Task<Result<GetInsuranceCompanyDTO>> UpdateInsuranceCompaniesAsync(UpdateInsuranceCompanyDTO dto)
        {
            // 1. Fetch existing entity with related Products and Contacts loaded (Eager Loading)
            var existingEntry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdWithIncludesAsync(dto.Id, x => x.Products, x => x.Contacts);

            if (existingEntry == null)
            {
                return Result<GetInsuranceCompanyDTO>.Failure("Insurance Company not found");
            }

            // 2. Map DTO changes onto the tracked entity
            _mapper.Map(dto, existingEntry);

            // 3. Audit trail tracking
            existingEntry.UpdatedBy = "SystemAudit";
            existingEntry.UpdatedDate = DateTime.Now;

            // 4. Persistence via Repository & Unit of Work
            var success = await _unitOfWork.InsuranceCompanyRepository.UpdateEntityAsync(existingEntry);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetInsuranceCompanyDTO>.Success(_mapper.Map<GetInsuranceCompanyDTO>(existingEntry), "Insurance Company updated successfully");
            }
            return Result<GetInsuranceCompanyDTO>.Failure("Failed to update Insurance Company");
        }

        public async Task<Result<bool>> ApproveInsuranceCompaniesAsync(int id)
        {
            var existingEntry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(id);

            if (existingEntry == null)
            {
                return Result<bool>.Failure("Insurance Company not found");
            }

            existingEntry.IsApproved = true;
            existingEntry.IsRejected = false;
            existingEntry.ApprovedBy = "Israa";
            existingEntry.ApprovedDate = DateTime.Now;

            var success = await _unitOfWork.InsuranceCompanyRepository.UpdateEntityAsync(existingEntry);
            if (success)
            {
                var accountResult = await _insuranceCompanyAccountService.GenerateAccountsAsync(existingEntry.Id);
                if (!accountResult.Succeeded)
                {
                    return Result<bool>.Failure($"Insurance Company approved, but account generation failed: {accountResult.Message}");
                }
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Company approved and accounts generated successfully");
            }
            return Result<bool>.Failure("Failed to approve Insurance Company");
        }

        public async Task<Result<bool>> RejectInsuranceCompaniesAsync(int id)
        {
            var success = await _unitOfWork.InsuranceCompanyRepository.RejectInsuranceCompaniesAsync(id);
            if (success)
            {
                return Result<bool>.Success(true, "Insurance Company rejected successfully");
            }
            return Result<bool>.Failure("Failed to reject Insurance Company or it is already processed");
        }
    }
}
