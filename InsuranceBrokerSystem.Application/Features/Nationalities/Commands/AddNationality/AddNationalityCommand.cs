namespace InsuranceBrokerSystem.Application.Features.Nationalities.Commands.AddNationality
{
    public class AddNationalityCommand : IRequest<Result<GetNationalityDTO>>
    {
        public AddNationalityDTO _addNationalityDTO { get; set; }
    }

    public class AddNationalityHandler : IRequestHandler<AddNationalityCommand, Result<GetNationalityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNationalityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetNationalityDTO>> Handle(AddNationalityCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.Nationality
            {
                Name = request._addNationalityDTO.Name,
                Code = request._addNationalityDTO.Code,
                Description = request._addNationalityDTO.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GNationality.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetNationalityDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetNationalityDTO>.Success(dto, "Nationality added successfully");
            }

            return Result<GetNationalityDTO>.Failure("Failed to add Nationality");
        }
    }
    public class AddNationalityValidator : AbstractValidator<AddNationalityCommand>
    {
        public AddNationalityValidator()
        {
            RuleFor(x => x._addNationalityDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._addNationalityDTO.Code).NotEmpty().WithMessage("Code is required");
        }
    }
}
