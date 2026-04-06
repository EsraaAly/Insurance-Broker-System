
namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.ApproveInsuranceCompany
{
    using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.GenerateInsuranceCompanyAccounts;

    public class ApproveInsuranceCompanyCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class ApproveInsuranceCompanyHandler : IRequestHandler<ApproveInsuranceCompanyCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;

        public ApproveInsuranceCompanyHandler(IUnitOfWork unitOfWork, ISender mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result<bool>> Handle(ApproveInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingEntry = await _unitOfWork.InsuranceCompanyRepository.GetEntityByIdAsync(request.Id);

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
                var accountResult = await _mediator.Send(new GenerateInsuranceCompanyAccountsCommand { CompanyId = existingEntry.Id });
                if (!accountResult.Succeeded)
                {
                    return Result<bool>.Failure($"Insurance Company approved, but account generation failed: {accountResult.Message}");
                }
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Company approved and accounts generated successfully");
            }
            return Result<bool>.Failure("Failed to approve Insurance Company");
        }
    }
}
