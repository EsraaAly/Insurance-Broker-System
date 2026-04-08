namespace InsuranceBrokerSystem.Application.Features.Locations.Commands.DeleteLocation
{
    public class DeleteLocationCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLocationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GLocation.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Location not found");
            }

            entity.IsDeleted = true;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var deletedEntity = await _unitOfWork.GLocation.UpdateEntityAsync(entity);
            if (deletedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Location deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete Location");
        }
    }
}
