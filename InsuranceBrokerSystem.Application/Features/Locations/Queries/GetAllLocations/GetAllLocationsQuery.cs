using InsuranceBrokerSystem.Application.DTOs.Master_Table.Location;

namespace InsuranceBrokerSystem.Application.Features.Locations.Queries.GetAllLocations
{
    public class GetAllLocationsQuery : IRequest<Result<IEnumerable<GetLocationDTO>>>
    {
    }

    public class GetAllLocationsHandler : IRequestHandler<GetAllLocationsQuery, Result<IEnumerable<GetLocationDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLocationsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<GetLocationDTO>>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GLocation.GetAllEntitytiesAsync();
            var dtos = entities.Select(entity => new GetLocationDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                Description = entity.Description
            });

            return Result<IEnumerable<GetLocationDTO>>.Success(dtos, "Locations retrieved successfully");
        }
    }

    
}
