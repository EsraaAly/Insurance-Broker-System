namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.RejectInsuranceCompany
{
    public class RejectInsuranceCompanyCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class RejectInsuranceCompanyHandler : IRequestHandler<RejectInsuranceCompanyCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectInsuranceCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RejectInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            var success = await _unitOfWork.InsuranceCompanyRepository.RejectInsuranceCompaniesAsync(request.Id);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Company rejected successfully");
            }
            return Result<bool>.Failure("Failed to reject Insurance Company or it is already processed");
        }
    }
}
