namespace InsuranceBrokerSystem.Application.Features.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<Result<GetLocationDTO>>
    {
        public UpdateLocationDTO _updateLocationDTO { get; set; }
    }

    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, Result<GetLocationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLocationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetLocationDTO>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GLocation.GetEntityByIdAsync(request._updateLocationDTO.Id);
            if (entity == null)
            {
                return Result<GetLocationDTO>.Failure("Location not found");
            }

            entity.Name = request._updateLocationDTO.Name;
            entity.Code = request._updateLocationDTO.Code;
            entity.Description = request._updateLocationDTO.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GLocation.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetLocationDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetLocationDTO>.Success(dto, "Location updated successfully");
            }

            return Result<GetLocationDTO>.Failure("Failed to update Location");
        }
    }
    public class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationValidator()
        {
            RuleFor(x => x._updateLocationDTO.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x._updateLocationDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._updateLocationDTO.Code).NotEmpty().WithMessage("Code is required");
        }
    }
}
