namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.DeleteSourceOfIncome
{
    public class DeleteSourceOfIncomeCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteSourceOfIncomeHandler : IRequestHandler<DeleteSourceOfIncomeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSourceOfIncomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteSourceOfIncomeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GSourceOfIncome.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Source Of Income not found");
            }

            entity.IsDeleted = true;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var deletedEntity = await _unitOfWork.GSourceOfIncome.UpdateEntityAsync(entity);
            if (deletedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Source Of Income deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete Source Of Income");
        }
    }
    public class DeleteSourceOfIncomeValidator : AbstractValidator<DeleteSourceOfIncomeCommand>
    {
        public DeleteSourceOfIncomeValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
