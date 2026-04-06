namespace InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.DeleteInsuranceCompany
{
    public class DeleteInsuranceCompanyCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteInsuranceCompanyHandler : IRequestHandler<DeleteInsuranceCompanyCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInsuranceCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            var success = await _unitOfWork.InsuranceCompanyRepository.DeleteEntityAsync(request.Id);

            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Company deleted successfully");
            }
            return Result<bool>.Failure("Failed to delete Insurance Company or it does not exist");
        }
    }
}
