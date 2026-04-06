namespace InsuranceBrokerSystem.Application.Features.InsuranceClasses.Queries.GetAllInsuranceClasses
{
    public class GetAllInsuranceClassesQuery : IRequest<Result<List<GetInsuranceClassDTO>>>
    {
    }

    public class GetAllInsuranceClassesHandler : IRequestHandler<GetAllInsuranceClassesQuery, Result<List<GetInsuranceClassDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllInsuranceClassesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceClassDTO>>> Handle(GetAllInsuranceClassesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GInsuranceClass.GetAllEntitytiesAsync();
            if (entities == null)
            {
                return Result<List<GetInsuranceClassDTO>>.Failure("No insurance classes found");
            }

            var dtos = entities.Select(i => new GetInsuranceClassDTO
            {
                Id = i.Id,
                ClassName = i.ClassName,
                Abbreviation = i.Abbreviation,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                UpdatedBy = i.UpdatedBy,
                UpdatedDate = i.UpdatedDate,
            }).ToList();

            return Result<List<GetInsuranceClassDTO>>.Success(dtos);
        }
    }
}
