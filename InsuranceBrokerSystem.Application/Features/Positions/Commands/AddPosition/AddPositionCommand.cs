using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;
using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Positions.Commands.AddPosition
{
    public class AddPositionCommand : IRequest<Result<GetPositionDTO>>
    {
        public AddPositionDTO _addPositionDTO { get; set; }
    }

    public class AddPositionHandler : IRequestHandler<AddPositionCommand, Result<GetPositionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPositionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPositionDTO>> Handle(AddPositionCommand request, CancellationToken cancellationToken)
        {
            // Check if position code already exists using generic repository
            var allPositions = await _unitOfWork.GPosition.GetAllEntitytiesAsync();
            var existingPosition = allPositions.FirstOrDefault(p => p.Code == request._addPositionDTO.Code);
            if (existingPosition != null)
            {
                return Result<GetPositionDTO>.Failure($"Position with code '{request._addPositionDTO.Code}' already exists");
            }

            var entity = new Domain.Entities.MasterTable.Position
            {
                Name = request._addPositionDTO.Name,
                Code = request._addPositionDTO.Code,
                Description = request._addPositionDTO.Description,
                Level = request._addPositionDTO.Level,
                IsActive = request._addPositionDTO.IsActive,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GPosition.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetPositionDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetPositionDTO>.Success(dto, "Position added successfully");
            }

            return Result<GetPositionDTO>.Failure("Failed to add Position");
        }
    }

    public class AddPositionValidator : AbstractValidator<AddPositionCommand>
    {
        public AddPositionValidator()
        {
            RuleFor(x => x._addPositionDTO.Name)
                .NotEmpty().WithMessage("Position name is required")
                .MaximumLength(100).WithMessage("Position name cannot exceed 100 characters");

            RuleFor(x => x._addPositionDTO.Code)
                .NotEmpty().WithMessage("Position code is required")
                .MaximumLength(10).WithMessage("Position code cannot exceed 10 characters")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("Position code can only contain alphanumeric characters");

            RuleFor(x => x._addPositionDTO.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x._addPositionDTO.Level)
                .GreaterThan(0).WithMessage("Level must be greater than 0")
                .LessThan(10).WithMessage("Level must be less than 10");
        }
    }
}
