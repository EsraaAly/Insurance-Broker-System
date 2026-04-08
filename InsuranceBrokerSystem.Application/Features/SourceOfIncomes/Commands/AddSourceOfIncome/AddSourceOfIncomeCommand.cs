namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.AddSourceOfIncome
{
    public class AddSourceOfIncomeCommand : IRequest<Result<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddSourceOfIncomeHandler : IRequestHandler<AddSourceOfIncomeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSourceOfIncomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddSourceOfIncomeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.SourceOfIncome
            {
                Name = request.Name,
                Description = request.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GSourceOfIncome.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Source Of Income added successfully");
            }

            return Result<bool>.Failure("Failed to add Source Of Income");
        }
    }
}
