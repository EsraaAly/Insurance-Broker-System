namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.UpdatePolicyType
{
    public class UpdatePolicyTypeCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePolicyTypeHandler : IRequestHandler<UpdatePolicyTypeCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePolicyTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdatePolicyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GPolicyType.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Policy Type not found");
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GPolicyType.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Policy Type updated successfully");
            }

            return Result<bool>.Failure("Failed to update Policy Type");
        }
    }
}
