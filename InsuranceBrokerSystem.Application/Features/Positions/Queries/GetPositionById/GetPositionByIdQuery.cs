using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;
using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Positions.Queries.GetPositionById
{
    public class GetPositionByIdQuery : IRequest<Result<GetPositionDTO>>
    {
        public int Id { get; set; }
    }

    public class GetPositionByIdHandler : IRequestHandler<GetPositionByIdQuery, Result<GetPositionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPositionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPositionDTO>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var position = await _unitOfWork.GPosition.GetEntityByIdAsync(request.Id);
            if (position == null)
            {
                return Result<GetPositionDTO>.Failure("Position not found");
            }

            var dto = position.Adapt<GetPositionDTO>();
            return Result<GetPositionDTO>.Success(dto, "Position retrieved successfully");
        }
    }

    public class GetPositionByIdValidator : AbstractValidator<GetPositionByIdQuery>
    {
        public GetPositionByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Valid position ID is required");
        }
    }
}
