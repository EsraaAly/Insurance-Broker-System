namespace InsuranceBrokerSystem.Application.Features.Locations.Queries.GetLocationById
{
    public class GetLocationByIdQuery : IRequest<Result<LocationDTO>>
    {
        public int Id { get; set; }
    }

    public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, Result<LocationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLocationByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LocationDTO>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GLocation.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<LocationDTO>.Failure("Location not found");
            }

            var dto = new LocationDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                Description = entity.Description
            };

            return Result<LocationDTO>.Success(dto, "Location retrieved successfully");
        }
    }

    public class LocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
