namespace InsuranceBrokerSystem.Application.Features.Nationalities.Commands.DeleteNationality
{
    public class DeleteNationalityCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteNationalityHandler : IRequestHandler<DeleteNationalityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNationalityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteNationalityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GNationality.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<bool>.Failure("Nationality not found");
            }

            entity.IsDeleted = true;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            var deletedEntity = await _unitOfWork.GNationality.UpdateEntityAsync(entity);
            if (deletedEntity != null)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Nationality deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete Nationality");
        }
    }
    public class DeleteNationalityValidator : AbstractValidator<DeleteNationalityCommand>
    {
        public DeleteNationalityValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
