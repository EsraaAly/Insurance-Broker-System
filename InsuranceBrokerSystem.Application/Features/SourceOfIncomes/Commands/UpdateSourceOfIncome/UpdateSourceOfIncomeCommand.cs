

namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.UpdateSourceOfIncome
{
    public class UpdateSourceOfIncomeCommand : IRequest<Result<GetSourceOfIncomeDTO>>
    {
        public UpdateSourceOfIncomeDTO _updateSourceOfIncomeDTO { get; set; }
    }

    public class UpdateSourceOfIncomeHandler : IRequestHandler<UpdateSourceOfIncomeCommand, Result<GetSourceOfIncomeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSourceOfIncomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetSourceOfIncomeDTO>> Handle(UpdateSourceOfIncomeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var entity = await _unitOfWork.GSourceOfIncome.GetEntityByIdAsync(request._updateSourceOfIncomeDTO.Id);

            if (entity == null)
            {
                return Result<GetSourceOfIncomeDTO>.Failure("Source Of Income not found");
            }

            entity.Name = request._updateSourceOfIncomeDTO.Name;
            entity.Description = request._updateSourceOfIncomeDTO.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GSourceOfIncome.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetSourceOfIncomeDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetSourceOfIncomeDTO>.Success(dto, "Source Of Income updated successfully");
            }

            return Result<GetSourceOfIncomeDTO>.Failure("Failed to update Source Of Income");
        }
    }
    public class UpdateSourceOfIncomeValidator : AbstractValidator<UpdateSourceOfIncomeCommand>
    {
        public UpdateSourceOfIncomeValidator()
        {
            RuleFor(x => x._updateSourceOfIncomeDTO.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x._updateSourceOfIncomeDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x._updateSourceOfIncomeDTO.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
