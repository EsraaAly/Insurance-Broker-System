
namespace InsuranceBrokerSystem.Application.Services.Financial
{
    public class InsuranceCompanyAccountService : IInsuranceCompanyAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public InsuranceCompanyAccountService(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Result<bool>> GenerateAccountsAsync(int companyId)
        {
            var company = await _unitOfWork.InsuranceCompany.GetEntityByIdAsync(companyId);
            if (company == null)
            {
                return Result<bool>.Failure("Insurance Company not found");
            }

            var accountDefinitions = new List<(string Suffix, int ParentId, Action<string> SetField)>
            {
                ("Commission Accrued", await GetParentIdByCodeAsync("002-001-001-000"), val => company.AccNoCommAccrued = val),
                ("Commission Due", await GetParentIdByCodeAsync("002-001-001-000"), val => company.AccNoCommDue = val),
                ("VAT Accrued", await GetParentIdByCodeAsync("002-001-002-000"), val => company.AccNoVATAccrued = val),
                ("VAT Receivable", await GetParentIdByCodeAsync("001-001-002-000"), val => company.AccNoVATReceivable = val),
                ("Gross Premium", await GetParentIdByCodeAsync("004-001-001-000"), val => company.AccNoGrossPremium = val),
                ("Gross VAT", await GetParentIdByCodeAsync("004-001-002-000"), val => company.AccNoGrossVAT = val),
                ("Net Premium", await GetParentIdByCodeAsync("004-001-001-000"), val => company.AccNoNetPremium = val),
                ("UW VAT Payable", await GetParentIdByCodeAsync("002-001-002-000"), val => company.AccNoUWVATPayable = val)
            };

            bool success = await ExecuteGenerationAsync(company, accountDefinitions);
            if (success)
            {
                return Result<bool>.Success(true, "Accounts generated successfully for the insurance company");
            }

            return Result<bool>.Failure("Failed to generate some or all accounts for the insurance company");
        }

        private async Task<int> GetParentIdByCodeAsync(string code)
        {
            var account = await _unitOfWork.AccountNumber.GetByExpressionAsync(a => a.AccountNumber.Trim().ToLower() == code.Trim().ToLower());
            return account?.Id ?? 0;
        }

        private async Task<bool> ExecuteGenerationAsync(InsuranceCompany company, List<(string Suffix, int ParentId, Action<string> SetField)> accountDefinitions)
        {
            foreach (var def in accountDefinitions)
            {
                if (def.ParentId == 0) continue;

                var parent = await _unitOfWork.AccountNumber.GetEntityByIdAsync(def.ParentId);
                if (parent == null) continue;

                var account = new Account
                {
                    AccountNumber = await _unitOfWork.AccountNumber.GenerateAsync(parent, parent.Children,(int)parent.AccountType),
                    AccountName = $"{company.Abbreviation} - {def.Suffix}",
                    Description = $"{def.Suffix} account for {company.CompanyName}",
                    ParentId = def.ParentId,
                    AccountType = parent.AccountType,
                    Level = parent.Level+1,
                    IsPostable = true
                };

                var result = await _unitOfWork.AccountNumber.AddEntityAsync(account);
                if (result != null)
                {
                    def.SetField(result.AccountNumber);
                }
            }

            await _unitOfWork.InsuranceCompany.UpdateEntityAsync(company);
            //await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
