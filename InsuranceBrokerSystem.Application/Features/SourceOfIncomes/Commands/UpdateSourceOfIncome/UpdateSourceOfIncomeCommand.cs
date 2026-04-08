namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.UpdateSourceOfIncome
{
    public class UpdateSourceOfIncomeCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSourceOfIncomeHandler : IRequestHandler<UpdateSourceOfIncomeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSourceOfIncomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateSourceOfIncomeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GSourceOfIncome.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Source Of Income not found");
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GSourceOfIncome.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Source Of Income updated successfully");
            }

            return Result<bool>.Failure("Failed to update Source Of Income");
        }
    }
}
