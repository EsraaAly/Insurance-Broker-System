using InsuranceBrokerSystem.Application.DTOs.Master_Table.PolicyType;

namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Queries.GetAllPolicyTypes
{
    public class GetAllPolicyTypesQuery : IRequest<Result<IEnumerable<GetPolicyTypeDTO>>>
    {
    }

    public class GetAllPolicyTypesHandler : IRequestHandler<GetAllPolicyTypesQuery, Result<IEnumerable<GetPolicyTypeDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPolicyTypesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<GetPolicyTypeDTO>>> Handle(GetAllPolicyTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GPolicyType.GetAllEntitytiesAsync();
            var dtos = entities.Select(entity => new GetPolicyTypeDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            });

            return Result<IEnumerable<GetPolicyTypeDTO>>.Success(dtos, "Policy Types retrieved successfully");
        }
    }

    
}
