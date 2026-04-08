namespace InsuranceBrokerSystem.Application.Features.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLocationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GLocation.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Location not found");
            }

            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.Description = request.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GLocation.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Location updated successfully");
            }

            return Result<bool>.Failure("Failed to update Location");
        }
    }
}
