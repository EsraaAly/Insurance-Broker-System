namespace InsuranceBrokerSystem.Application.Features.Nationalities.Commands.UpdateNationality
{
    public class UpdateNationalityCommand : IRequest<Result<GetNationalityDTO>>
    {
        public UpdateNationalityDTO _updateNationalityDTO { get; set; }
    }

    public class UpdateNationalityHandler : IRequestHandler<UpdateNationalityCommand, Result<GetNationalityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNationalityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetNationalityDTO>> Handle(UpdateNationalityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GNationality.GetEntityByIdAsync(request._updateNationalityDTO.Id);
            if (entity == null)
            {
                return Result<GetNationalityDTO>.Failure("Nationality not found");
            }

            entity.Name = request._updateNationalityDTO.Name;
            entity.Code = request._updateNationalityDTO.Code;
            entity.Description = request._updateNationalityDTO.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GNationality.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetNationalityDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetNationalityDTO>.Success(dto, "Nationality updated successfully");
            }

            return Result<GetNationalityDTO>.Failure("Failed to update Nationality");
        }
    }
    public class UpdateNationalityValidator : AbstractValidator<UpdateNationalityCommand>
    {
        public UpdateNationalityValidator()
        {
            RuleFor(x => x._updateNationalityDTO.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x._updateNationalityDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._updateNationalityDTO.Code).NotEmpty().WithMessage("Code is required");
        }
    }
}
