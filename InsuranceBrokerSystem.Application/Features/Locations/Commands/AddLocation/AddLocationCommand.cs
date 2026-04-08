namespace InsuranceBrokerSystem.Application.Features.Locations.Commands.AddLocation
{
    public class AddLocationCommand : IRequest<Result<bool>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class AddLocationHandler : IRequestHandler<AddLocationCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddLocationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.MasterTable.Location
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

            var addedEntity = await _unitOfWork.GLocation.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Location added successfully");
            }

            return Result<bool>.Failure("Failed to add Location");
        }
    }
}
