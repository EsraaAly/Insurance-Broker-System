namespace InsuranceBrokerSystem.Application.Features.InsuranceClasses.Commands.AddInsuranceClass
{
    public class AddInsuranceClassCommand : IRequest<Result<bool>>
    {
        public AddInsuranceClassDTO _addInsuranceClassDTO { get; set; }
        
        // Keep existing properties for backward compatibility
        public string ClassName { get; set; }
        public string Abbreviation { get; set; }
    }

    public class AddInsuranceClassHandler : IRequestHandler<AddInsuranceClassCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddInsuranceClassHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddInsuranceClassCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.InsuranceClass
            {
                ClassName = request.ClassName,
                Abbreviation = request.Abbreviation,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GInsuranceClass.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Insurance Class added successfully");
            }

            return Result<bool>.Failure("Failed to add Insurance Class");
        }
    }
}
