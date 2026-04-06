namespace InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Commands.DeleteInsuranceLOB
{
    public class DeleteInsuranceLOBCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteInsuranceLOBHandler : IRequestHandler<DeleteInsuranceLOBCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInsuranceLOBHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteInsuranceLOBCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.InsuranceLOBRepository.GetEntityByIdAsync(request.Id);

            if (entity != null)
            {
                var success = await _unitOfWork.InsuranceLOBRepository.DeleteEntityAsync(request.Id);
                if (success)
                {
                    await _unitOfWork.CommitAsync();
                    return Result<bool>.Success(true, "Line of Business deleted successfully");
                }
                return Result<bool>.Failure("Failed to delete Line of Business");
            }
            return Result<bool>.Failure("Line of Business not found");
        }
    }
}
