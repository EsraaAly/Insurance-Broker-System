using InsuranceBrokerSystem.Application.DTOs.Master_Table.BusinessActivity;

namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Queries.GetAllBusinessActivities
{
    public class GetAllBusinessActivitiesQuery : IRequest<Result<IEnumerable<GetBusinessActivityDTO>>>
    {
    }

    public class GetAllBusinessActivitiesHandler : IRequestHandler<GetAllBusinessActivitiesQuery, Result<IEnumerable<GetBusinessActivityDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBusinessActivitiesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<GetBusinessActivityDTO>>> Handle(GetAllBusinessActivitiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GBusinessActivity.GetAllEntitytiesAsync();
            var dtos = entities.Select(entity => new GetBusinessActivityDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            });

            return Result<IEnumerable<GetBusinessActivityDTO>>.Success(dtos, "Business Activities retrieved successfully");
        }
    }

    
}
