namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.GenerateInsuranceCompanyAccounts
{
    public class GenerateInsuranceCompanyAccountsCommand : IRequest<Result<bool>>
    {
        public int CompanyId { get; set; }
    }

    public class GenerateInsuranceCompanyAccountsHandler : IRequestHandler<GenerateInsuranceCompanyAccountsCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateInsuranceCompanyAccountsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(GenerateInsuranceCompanyAccountsCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var company = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result<bool>.Failure("Insurance Company not found");
            }

            var parent1 = await GetParentIdByCodeAsync("002-001-001-000");
            var parent2 = await GetParentIdByCodeAsync("002-001-002-000");
            var parent3 = await GetParentIdByCodeAsync("001-001-002-000");
            var parent4 = await GetParentIdByCodeAsync("004-001-001-000");
            var parent5 = await GetParentIdByCodeAsync("004-001-002-000");

            var accountDefinitions = new List<(string Suffix, int ParentId, Action<string> SetField)>
            {
                ("Commission Accrued", parent1, val => company.AccNoCommAccrued = val),
                ("Commission Due",     parent1, val => company.AccNoCommDue = val),
                ("VAT Accrued",        parent2, val => company.AccNoVATAccrued = val),
                ("VAT Receivable",     parent3, val => company.AccNoVATReceivable = val),
                ("Gross Premium",      parent4, val => company.AccNoGrossPremium = val),
                ("Gross VAT",          parent5, val => company.AccNoGrossVAT = val),
                ("Net Premium",        parent4, val => company.AccNoNetPremium = val),
                ("UW VAT Payable",     parent2, val => company.AccNoUWVATPayable = val)
            };

            var Result = await ExecuteGenerationAsync(company, accountDefinitions);
            if (Result.Succeeded)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Accounts generated and mapped successfully.");
            }

            return Result<bool>.Failure("Failed to complete automated account registration.");
        }

        private async Task<int> GetParentIdByCodeAsync(string code)
        {
            var account = await _unitOfWork.AccountNumberRepository.GetByExpressionAsync(a => a.AccountNumber.Trim().ToLower() == code.Trim().ToLower());
            return account?.Id ?? 0;
        }

        private async Task<Result<bool>> ExecuteGenerationAsync(InsuranceCompany company, List<(string Suffix, int ParentId, Action<string> SetField)> accountDefinitions)
        {
            foreach (var def in accountDefinitions)
            {
                if (def.ParentId == 0) continue;

                var parent = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(def.ParentId);
                if (parent == null) continue;

                var account = new Account
                {
                    AccountNumber = await _unitOfWork.AccountNumberRepository.GenerateAsync(parent, parent.Children, (int)parent.AccountType),
                    AccountName = $"{company.Abbreviation} - {def.Suffix}",
                    Description = $"{def.Suffix} account for {company.CompanyName}",
                    ParentId = def.ParentId,
                    AccountType = parent.AccountType,
                    Level = parent.Level + 1,
                    IsPostable = true
                };

                var result = await _unitOfWork.AccountNumberRepository.AddEntityAsync(account);
                if (result == null)
                    return Result<bool>.Failure($"Failed to create account for {def.Suffix}");

                def.SetField(result.AccountNumber);
            }

            await _unitOfWork.InsuranceCompanyRepository.UpdateEntityAsync(company);

            return Result<bool>.Success(true);
        }
    }

    public class GenerateInsuranceCompanyAccountsValidator
    : AbstractValidator<GenerateInsuranceCompanyAccountsCommand>
    {
        public GenerateInsuranceCompanyAccountsValidator()
        {
            RuleFor(x => x.CompanyId)
             .GreaterThan(0)
             .WithMessage("CompanyId must exist");
        }
    }
}
