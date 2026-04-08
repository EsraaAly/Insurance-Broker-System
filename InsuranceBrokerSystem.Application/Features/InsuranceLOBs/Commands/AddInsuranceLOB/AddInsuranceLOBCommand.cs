namespace InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Commands.AddInsuranceLOB
{
    public class AddInsuranceLOBCommand : IRequest<Result<bool>>
    {
        public int ClassID { get; set; }
        public string LineOfBusiness { get; set; }
        public string Abbreviation { get; set; }
    }

    public class AddInsuranceLOBHandler : IRequestHandler<AddInsuranceLOBCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddInsuranceLOBHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddInsuranceLOBCommand request, CancellationToken cancellationToken)
        {
            var Insureanceclass = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(request.ClassID);
            var entity = new Domain.Entities.MasterTable.InsuranceLineOfBusiness
            {
                ClassID = request.ClassID,
                InsuranceClass = Insureanceclass,
                LineOfBusiness = request.LineOfBusiness,
                Abbreviation = request.Abbreviation,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };
            var addedEntity = await _unitOfWork.InsuranceLOBRepository.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Line of Business added successfully");
            }

            return Result<bool>.Failure("Failed to add Line of Business");
        }
    }
}
