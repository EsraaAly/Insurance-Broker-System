namespace InsuranceBrokerSystem.Application.Features.Locations.Queries.GetLocationById
{
    public class GetLocationByIdQuery : IRequest<Result<GetLocationDTO>>
    {
        public int Id { get; set; }
    }

    public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, Result<GetLocationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLocationByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetLocationDTO>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GLocation.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<GetLocationDTO>.Failure("Location not found");
            }

            var dto = entity.Adapt<GetLocationDTO>();

            return Result<GetLocationDTO>.Success(dto, "Location retrieved successfully");
        }
    }
    public class getLocationByIdValidator : AbstractValidator<GetLocationByIdQuery>
    {
        public getLocationByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

}
