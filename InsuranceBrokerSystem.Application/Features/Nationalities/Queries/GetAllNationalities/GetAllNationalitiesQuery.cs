namespace InsuranceBrokerSystem.Application.Features.Nationalities.Queries.GetAllNationalities
{
    public class GetAllNationalitiesQuery : IRequest<Result<IEnumerable<GetNationalityDTO>>>
    {
    }

    public class GetAllNationalitiesHandler : IRequestHandler<GetAllNationalitiesQuery, Result<IEnumerable<GetNationalityDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllNationalitiesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<GetNationalityDTO>>> Handle(GetAllNationalitiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GNationality.GetAllEntitytiesAsync();
            var dtos = entities.Adapt<IEnumerable<GetNationalityDTO>>();

            return Result<IEnumerable<GetNationalityDTO>>.Success(dtos, "Nationalities retrieved successfully");
        }
    }
}
