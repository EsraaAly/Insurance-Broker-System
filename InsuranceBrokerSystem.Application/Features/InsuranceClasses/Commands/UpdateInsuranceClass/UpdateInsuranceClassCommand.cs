namespace InsuranceBrokerSystem.Application.Features.InsuranceClasses.Commands.UpdateInsuranceClass
{
    public class UpdateInsuranceClassCommand : IRequest<Result<GetInsuranceClassDTO>>
    {
        public UpdateInsuranceClassDTO _updateInsuranceClassDTO { get; set; }
        
        // Keep existing properties for backward compatibility
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Abbreviation { get; set; }
    }

    public class UpdateInsuranceClassHandler : IRequestHandler<UpdateInsuranceClassCommand, Result<GetInsuranceClassDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInsuranceClassHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceClassDTO>> Handle(UpdateInsuranceClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(request.Id);

            if (entity == null)
            {
                return Result<GetInsuranceClassDTO>.Failure("Insurance Class not found");
            }

            entity.ClassName = request.ClassName;
            entity.Abbreviation = request.Abbreviation;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            bool isUpdated = await _unitOfWork.GInsuranceClass.UpdateEntityAsync(entity);
            if (isUpdated)
            {
                await _unitOfWork.CommitAsync();
                var updatedDto = new GetInsuranceClassDTO
                {
                    Id = entity.Id,
                    ClassName = entity.ClassName,
                    Abbreviation = entity.Abbreviation,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                };
                return Result<GetInsuranceClassDTO>.Success(updatedDto, "Insurance Class updated successfully");
            }

            return Result<GetInsuranceClassDTO>.Failure("Failed to update Insurance Class");
        }
    }
}
