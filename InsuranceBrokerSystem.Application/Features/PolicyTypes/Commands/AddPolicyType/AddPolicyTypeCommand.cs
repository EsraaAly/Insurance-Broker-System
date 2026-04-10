namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.AddPolicyType
{
    public class AddPolicyTypeCommand : IRequest<Result<GetPolicyTypeDTO>>
    {
        public AddPolicyTypeDTO _addPolicyTypeDTO { get; set; }
    }

    public class AddPolicyTypeHandler : IRequestHandler<AddPolicyTypeCommand, Result<GetPolicyTypeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPolicyTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPolicyTypeDTO>> Handle(AddPolicyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.PolicyType
            {
                Name = request._addPolicyTypeDTO.Name,
                Description = request._addPolicyTypeDTO.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GPolicyType.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetPolicyTypeDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetPolicyTypeDTO>.Success(dto, "Policy Type added successfully");
            }

            return Result<GetPolicyTypeDTO>.Failure("Failed to add Policy Type");
        }
    }
    public class AddPolicyTypeValidator : AbstractValidator<AddPolicyTypeCommand>
    {
        public AddPolicyTypeValidator()
        {
            RuleFor(x => x._addPolicyTypeDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._addPolicyTypeDTO.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
