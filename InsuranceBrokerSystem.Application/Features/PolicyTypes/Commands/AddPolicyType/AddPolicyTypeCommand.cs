namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.AddPolicyType
{
    public class AddPolicyTypeCommand : IRequest<Result<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddPolicyTypeHandler : IRequestHandler<AddPolicyTypeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPolicyTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddPolicyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.PolicyType
            {
                Name = request.Name,
                Description = request.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GPolicyType.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Policy Type added successfully");
            }

            return Result<bool>.Failure("Failed to add Policy Type");
        }
    }
}
