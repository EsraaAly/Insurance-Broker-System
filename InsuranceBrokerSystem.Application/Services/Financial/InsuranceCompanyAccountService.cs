
namespace InsuranceBrokerSystem.Application.Services.Financial
{
    public class InsuranceCompanyAccountService : IInsuranceCompanyAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceCompanyAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Initiates the automated generation of GL accounts for an Insurance Company.
        /// Orchestrates the template definition and internal execution logic.
        /// </summary>
        public async Task<Result<bool>> GenerateAccountsAsync(int companyId)
        {
            // 1. Resolve insurance company and its metadata
            var company = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(companyId);
            if (company == null)
            {
                return Result<bool>.Failure("Insurance Company not found");
            }

            // 2. Define Account Templates (Strategy: Suffix, Parent, Mapping Action)
            var accountDefinitions = new List<(string Suffix, int ParentId, Action<string> SetField)>
            {
                ("Commission Accrued", await GetParentIdByCodeAsync("002-001-001-000"), val => company.AccNoCommAccrued = val),
                ("Commission Due",     await GetParentIdByCodeAsync("002-001-001-000"), val => company.AccNoCommDue = val),
                ("VAT Accrued",        await GetParentIdByCodeAsync("002-001-002-000"), val => company.AccNoVATAccrued = val),
                ("VAT Receivable",     await GetParentIdByCodeAsync("001-001-002-000"), val => company.AccNoVATReceivable = val),
                ("Gross Premium",      await GetParentIdByCodeAsync("004-001-001-000"), val => company.AccNoGrossPremium = val),
                ("Gross VAT",          await GetParentIdByCodeAsync("004-001-002-000"), val => company.AccNoGrossVAT = val),
                ("Net Premium",        await GetParentIdByCodeAsync("004-001-001-000"), val => company.AccNoNetPremium = val),
                ("UW VAT Payable",     await GetParentIdByCodeAsync("002-001-002-000"), val => company.AccNoUWVATPayable = val)
            };

            // 3. Execute transactional generation
            bool success = await ExecuteGenerationAsync(company, accountDefinitions);
            if (success)
            {
                return Result<bool>.Success(true, "Accounts generated and mapped successfully.");
            }

            return Result<bool>.Failure("Failed to complete automated account registration.");
        }

        private async Task<int> GetParentIdByCodeAsync(string code)
        {
            var account = await _unitOfWork.AccountNumberRepository.GetByExpressionAsync(a => a.AccountNumber.Trim().ToLower() == code.Trim().ToLower());
            return account?.Id ?? 0;
        }

        /// <summary>
        /// Internal implementation of the recursive account creation and sibling-based numbering.
        /// </summary>
        private async Task<bool> ExecuteGenerationAsync(InsuranceCompany company, List<(string Suffix, int ParentId, Action<string> SetField)> accountDefinitions)
        {
            foreach (var def in accountDefinitions)
            {
                if (def.ParentId == 0) continue;

                var parent = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(def.ParentId);
                if (parent == null) continue;

                var account = new Account
                {
                    // 🔥 Business Rule: Auto-calculate unique suffix based on existing siblings
                    AccountNumber = await _unitOfWork.AccountNumberRepository.GenerateAsync(parent, parent.Children, (int)parent.AccountType),
                    AccountName = $"{company.Abbreviation} - {def.Suffix}",
                    Description = $"{def.Suffix} account for {company.CompanyName}",
                    ParentId = def.ParentId,
                    AccountType = parent.AccountType,
                    Level = parent.Level + 1,
                    IsPostable = true
                };

                var result = await _unitOfWork.AccountNumberRepository.AddEntityAsync(account);
                if (result != null)
                {
                    def.SetField(result.AccountNumber);
                }
            }

            // Update company with newly assigned account numbers
            await _unitOfWork.InsuranceCompanyRepository.UpdateEntityAsync(company);
            return true;
        }
    }
}
