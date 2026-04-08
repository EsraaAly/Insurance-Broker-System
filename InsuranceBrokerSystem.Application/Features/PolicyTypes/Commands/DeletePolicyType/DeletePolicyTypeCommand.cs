namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.DeletePolicyType
{
    public class DeletePolicyTypeCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeletePolicyTypeHandler : IRequestHandler<DeletePolicyTypeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePolicyTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletePolicyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GPolicyType.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Policy Type not found");
            }

            entity.IsDeleted = true;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var deletedEntity = await _unitOfWork.GPolicyType.UpdateEntityAsync(entity);
            if (deletedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Policy Type deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete Policy Type");
        }
    }
}
