namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.AddSourceOfIncome
{
    public class AddSourceOfIncomeCommand : IRequest<Result<GetSourceOfIncomeDTO>>
    {
        public AddSourceOfIncomeDTO _addSourceOfIncomeDTO { get; set; }
    }

    public class AddSourceOfIncomeHandler : IRequestHandler<AddSourceOfIncomeCommand, Result<GetSourceOfIncomeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSourceOfIncomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetSourceOfIncomeDTO>> Handle(AddSourceOfIncomeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.SourceOfIncome
            {
                Name = request._addSourceOfIncomeDTO.Name,
                Description = request._addSourceOfIncomeDTO.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GSourceOfIncome.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetSourceOfIncomeDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetSourceOfIncomeDTO>.Success(dto, "Source Of Income added successfully");
            }

            return Result<GetSourceOfIncomeDTO>.Failure("Failed to add Source Of Income");
        }
    }
    public class AddSourceOfIncomeValidator : AbstractValidator<AddSourceOfIncomeCommand>
    {
        public AddSourceOfIncomeValidator()
        {
            RuleFor(x => x._addSourceOfIncomeDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._addSourceOfIncomeDTO.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
