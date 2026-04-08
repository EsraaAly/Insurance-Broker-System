namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.AddBusinessActivity
{
    public class AddBusinessActivityCommand : IRequest<Result<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddBusinessActivityHandler : IRequestHandler<AddBusinessActivityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBusinessActivityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddBusinessActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.BusinessActivity
            {
                Name = request.Name,
                Description = request.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GBusinessActivity.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Business Activity added successfully");
            }

            return Result<bool>.Failure("Failed to add Business Activity");
        }
    }
}
