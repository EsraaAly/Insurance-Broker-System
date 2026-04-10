
namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.UpdatePolicyType
{
    public class UpdatePolicyTypeCommand : IRequest<Result<GetPolicyTypeDTO>>
    {
        public UpdatePolicyTypeDTO _updatePolicyTypeDTO { get; set; }
    }

    public class UpdatePolicyTypeHandler : IRequestHandler<UpdatePolicyTypeCommand, Result<GetPolicyTypeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePolicyTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPolicyTypeDTO>> Handle(UpdatePolicyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GPolicyType.GetEntityByIdAsync(request._updatePolicyTypeDTO.Id);
            if (entity == null)
            {
                return Result<GetPolicyTypeDTO>.Failure("Policy Type not found");
            }

            entity.Name = request._updatePolicyTypeDTO.Name;
            entity.Description = request._updatePolicyTypeDTO.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GPolicyType.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetPolicyTypeDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetPolicyTypeDTO>.Success(dto, "Policy Type updated successfully");
            }

            return Result<GetPolicyTypeDTO>.Failure("Failed to update Policy Type");
        }
    }
    public class UpdatePolicyTypeValidator : AbstractValidator<UpdatePolicyTypeCommand>
    {
        public UpdatePolicyTypeValidator()
        {
            RuleFor(x => x._updatePolicyTypeDTO.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x._updatePolicyTypeDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._updatePolicyTypeDTO.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
