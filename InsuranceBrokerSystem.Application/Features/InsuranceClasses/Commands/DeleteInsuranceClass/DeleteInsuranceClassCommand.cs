namespace InsuranceBrokerSystem.Application.Features.InsuranceClasses.Commands.DeleteInsuranceClass
{
    public class DeleteInsuranceClassCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteInsuranceClassHandler : IRequestHandler<DeleteInsuranceClassCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInsuranceClassHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteInsuranceClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(request.Id);

            if (entity == null)
            {
                return Result<bool>.Failure("Insurance Class not found");
            }

            var success = await _unitOfWork.GInsuranceClass.DeleteEntityAsync(request.Id);

            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Class deleted successfully");
            }
            return Result<bool>.Failure("Failed to delete Insurance Class");
        }
    }
}
