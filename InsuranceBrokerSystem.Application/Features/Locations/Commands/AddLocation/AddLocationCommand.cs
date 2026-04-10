namespace InsuranceBrokerSystem.Application.Features.Locations.Commands.AddLocation
{
    public class AddLocationCommand : IRequest<Result<GetLocationDTO>>
    {
        public AddLocationDTO _addLocationDTO { get; set; }
    }

    public class AddLocationHandler : IRequestHandler<AddLocationCommand, Result<GetLocationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddLocationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetLocationDTO>> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.Location
            {
                Name = request._addLocationDTO.Name,
                Code = request._addLocationDTO.Code,
                Description = request._addLocationDTO.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GLocation.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetLocationDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetLocationDTO>.Success(dto, "Location added successfully");
            }

            return Result<GetLocationDTO>.Failure("Failed to add Location");
        }
    }
    public class AddLocationValidator : AbstractValidator<AddLocationCommand>
    {
        public AddLocationValidator()
        {
            RuleFor(x => x._addLocationDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._addLocationDTO.Code).NotEmpty().WithMessage("Code is required");
        }
    }
}
