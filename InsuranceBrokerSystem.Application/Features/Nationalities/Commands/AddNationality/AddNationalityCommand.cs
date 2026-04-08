namespace InsuranceBrokerSystem.Application.Features.Nationalities.Commands.AddNationality
{
    public class AddNationalityCommand : IRequest<Result<bool>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class AddNationalityHandler : IRequestHandler<AddNationalityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNationalityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddNationalityCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.Nationality
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                CreatedBy = "Israa",
                CreatedDate = DateTime.Now,
                UpdatedBy = "",
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GNationality.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Nationality added successfully");
            }

            return Result<bool>.Failure("Failed to add Nationality");
        }
    }
}
