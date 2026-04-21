namespace InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Commands.UpdateInsuranceLOB
{
    public class UpdateInsuranceLOBCommand : IRequest<Result<GetInsuranceLOBDTO>>
    {
        public UpdateInsuranceLOBDTO _updateInsuranceLOBDTO { get; set; }
        
        // Keep existing properties for backward compatibility
        public int Id { get; set; }
        public int ClassID { get; set; }
        public string LineOfBusiness { get; set; }
        public string Abbreviation { get; set; }
    }

    public class UpdateInsuranceLOBHandler : IRequestHandler<UpdateInsuranceLOBCommand, Result<GetInsuranceLOBDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInsuranceLOBHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceLOBDTO>> Handle(UpdateInsuranceLOBCommand request, CancellationToken cancellationToken)
        {
            var Insureanceclass = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(request.ClassID);
            var entity = await _unitOfWork.InsuranceLOBRepository.GetEntityByIdAsync(request.Id);

            if (entity == null)
            {
                return Result<GetInsuranceLOBDTO>.Failure("Line of Business not found");
            }

            entity.LineOfBusiness = request.LineOfBusiness;
            entity.Abbreviation = request.Abbreviation;
            entity.ClassID = request.ClassID;
            entity.InsuranceClass = Insureanceclass;

            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            bool isUpdated = await _unitOfWork.InsuranceLOBRepository.UpdateEntityAsync(entity);

            if (isUpdated)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetInsuranceLOBDTO>.Success(new GetInsuranceLOBDTO
                {
                    Id = entity.Id,
                    LineOfBusiness = entity.LineOfBusiness,
                    Abbreviation = entity.Abbreviation,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    ClassID = entity.ClassID
                }, "Line of Business updated successfully");
            }

            return Result<GetInsuranceLOBDTO>.Failure("Failed to update Line of Business");
        }
    }
}
