using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;
using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Positions.Queries.GetAllPositions
{
    public class GetAllPositionsQuery : IRequest<Result<List<GetPositionDTO>>>
    {
    }

    public class GetAllPositionsHandler : IRequestHandler<GetAllPositionsQuery, Result<List<GetPositionDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPositionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetPositionDTO>>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = await _unitOfWork.GPosition.GetAllEntitytiesAsync();
            var dtos = positions.Adapt<List<GetPositionDTO>>();
            
            return Result<List<GetPositionDTO>>.Success(dtos, "Positions retrieved successfully");
        }
    }
}
