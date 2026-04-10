namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.UpdateBusinessActivity
{
    public class UpdateBusinessActivityCommand : IRequest<Result<GetBusinessActivityDTO>>
    {
        public UpdateBusinessActivityDTO _updateBusinessActivityDTO { get; set; }
    }

    public class UpdateBusinessActivityHandler : IRequestHandler<UpdateBusinessActivityCommand, Result<GetBusinessActivityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBusinessActivityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetBusinessActivityDTO>> Handle(UpdateBusinessActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GBusinessActivity.GetEntityByIdAsync(request._updateBusinessActivityDTO.Id);
            if (entity == null)
            {
                return Result<GetBusinessActivityDTO>.Failure("Business Activity not found");
            }

            entity.Name = request._updateBusinessActivityDTO.Name;
            entity.Description = request._updateBusinessActivityDTO.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GBusinessActivity.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetBusinessActivityDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetBusinessActivityDTO>.Success(dto, "Business Activity updated successfully");
            }

            return Result<GetBusinessActivityDTO>.Failure("Failed to update Business Activity");
        }
    }
    public class UpdateBusinessActivityValidator : AbstractValidator<UpdateBusinessActivityCommand>
    {
        public UpdateBusinessActivityValidator()
        {
            RuleFor(x => x._updateBusinessActivityDTO.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x._updateBusinessActivityDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._updateBusinessActivityDTO.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
