namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.UpdateBusinessActivity
{
    public class UpdateBusinessActivityCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateBusinessActivityHandler : IRequestHandler<UpdateBusinessActivityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBusinessActivityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateBusinessActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GBusinessActivity.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Business Activity not found");
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GBusinessActivity.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Business Activity updated successfully");
            }

            return Result<bool>.Failure("Failed to update Business Activity");
        }
    }
}
