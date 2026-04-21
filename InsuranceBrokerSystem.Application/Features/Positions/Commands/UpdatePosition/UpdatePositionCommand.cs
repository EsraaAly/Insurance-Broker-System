using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;
using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest<Result<GetPositionDTO>>
    {
        public UpdatePositionDTO _updatePositionDTO { get; set; }
    }

    public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand, Result<GetPositionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePositionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPositionDTO>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var existingPosition = await _unitOfWork.GPosition.GetEntityByIdAsync(request._updatePositionDTO.Id);
            if (existingPosition == null)
            {
                return Result<GetPositionDTO>.Failure("Position not found");
            }

            // Check if code conflicts with another position using generic repository
            var allPositions = await _unitOfWork.GPosition.GetAllEntitytiesAsync();
            var positionWithSameCode = allPositions.FirstOrDefault(p => p.Code == request._updatePositionDTO.Code && p.Id != request._updatePositionDTO.Id);
            if (positionWithSameCode != null)
            {
                return Result<GetPositionDTO>.Failure($"Position with code '{request._updatePositionDTO.Code}' already exists");
            }

            existingPosition.Name = request._updatePositionDTO.Name;
            existingPosition.Code = request._updatePositionDTO.Code;
            existingPosition.Description = request._updatePositionDTO.Description;
            existingPosition.Level = request._updatePositionDTO.Level;
            existingPosition.IsActive = request._updatePositionDTO.IsActive;
            existingPosition.UpdatedBy = "System";
            existingPosition.UpdatedDate = DateTime.UtcNow;

            var updatedEntity = await _unitOfWork.GPosition.UpdateEntityAsync(existingPosition);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetPositionDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetPositionDTO>.Success(dto, "Position updated successfully");
            }

            return Result<GetPositionDTO>.Failure("Failed to update Position");
        }
    }

    public class UpdatePositionValidator : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionValidator()
        {
            RuleFor(x => x._updatePositionDTO.Id)
                .GreaterThan(0).WithMessage("Valid position ID is required");

            RuleFor(x => x._updatePositionDTO.Name)
                .NotEmpty().WithMessage("Position name is required")
                .MaximumLength(100).WithMessage("Position name cannot exceed 100 characters");

            RuleFor(x => x._updatePositionDTO.Code)
                .NotEmpty().WithMessage("Position code is required")
                .MaximumLength(10).WithMessage("Position code cannot exceed 10 characters")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("Position code can only contain alphanumeric characters");

            RuleFor(x => x._updatePositionDTO.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x._updatePositionDTO.Level)
                .GreaterThan(0).WithMessage("Level must be greater than 0")
                .LessThan(10).WithMessage("Level must be less than 10");
        }
    }
}
