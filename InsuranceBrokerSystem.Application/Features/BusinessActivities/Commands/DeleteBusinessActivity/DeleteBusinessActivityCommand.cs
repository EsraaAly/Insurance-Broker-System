namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.DeleteBusinessActivity
{
    public class DeleteBusinessActivityCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteBusinessActivityHandler : IRequestHandler<DeleteBusinessActivityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBusinessActivityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteBusinessActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GBusinessActivity.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Business Activity not found");
            }

            entity.IsDeleted = true;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var deletedEntity = await _unitOfWork.GBusinessActivity.UpdateEntityAsync(entity);
            if (deletedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Business Activity deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete Business Activity");
        }
    }
}
