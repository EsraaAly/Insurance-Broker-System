using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Positions.Commands.DeletePosition
{
    public class DeletePositionCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
    }

    public class DeletePositionHandler : IRequestHandler<DeletePositionCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePositionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await _unitOfWork.GPosition.GetEntityByIdAsync(request.Id);
            if (position == null)
            {
                return Result<string>.Failure("Position not found");
            }

            // Note: For simplicity, we're skipping the related entities check with generic repository
            // In a real implementation, you might want to add this check manually

            var deleted = await _unitOfWork.GPosition.DeleteEntityAsync(request.Id);
            if (deleted)
            {
                await _unitOfWork.CommitAsync();
                return Result<string>.Success("Position deleted successfully");
            }

            return Result<string>.Failure("Failed to delete Position");
        }
    }

    public class DeletePositionValidator : AbstractValidator<DeletePositionCommand>
    {
        public DeletePositionValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Valid position ID is required");
        }
    }
}
