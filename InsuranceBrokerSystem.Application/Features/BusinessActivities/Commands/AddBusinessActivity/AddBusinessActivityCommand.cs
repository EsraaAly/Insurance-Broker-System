namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.AddBusinessActivity
{
    public class AddBusinessActivityCommand : IRequest<Result<GetBusinessActivityDTO>>
    {
        public AddBusinessActivityDTO _addBusinessActivityDTO { get; set; }
    }

    public class AddBusinessActivityHandler : IRequestHandler<AddBusinessActivityCommand, Result<GetBusinessActivityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBusinessActivityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetBusinessActivityDTO>> Handle(AddBusinessActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.BusinessActivity
            {
                Name = request._addBusinessActivityDTO.Name,
                Description = request._addBusinessActivityDTO.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GBusinessActivity.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetBusinessActivityDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetBusinessActivityDTO>.Success(dto, "Business Activity added successfully");
            }

            return Result<GetBusinessActivityDTO>.Failure("Failed to add Business Activity");
        }
    }
    public class AddBusinessActivityValidator : AbstractValidator<AddBusinessActivityCommand>
    {
        public AddBusinessActivityValidator()
        {
            RuleFor(x => x._addBusinessActivityDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._addBusinessActivityDTO.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
