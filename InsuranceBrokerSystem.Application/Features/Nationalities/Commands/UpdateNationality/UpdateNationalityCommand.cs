namespace InsuranceBrokerSystem.Application.Features.Nationalities.Commands.UpdateNationality
{
    public class UpdateNationalityCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class UpdateNationalityHandler : IRequestHandler<UpdateNationalityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNationalityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateNationalityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GNationality.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Nationality not found");
            }

            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.Description = request.Description;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var updatedEntity = await _unitOfWork.GNationality.UpdateEntityAsync(entity);
            if (updatedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Nationality updated successfully");
            }

            return Result<bool>.Failure("Failed to update Nationality");
        }
    }
}
