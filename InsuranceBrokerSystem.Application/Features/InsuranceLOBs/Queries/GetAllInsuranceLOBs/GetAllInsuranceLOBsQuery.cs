namespace InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Queries.GetAllInsuranceLOBs
{
    public class GetAllInsuranceLOBsQuery : IRequest<Result<List<GetInsuranceLOBDTO>>>
    {
    }

    public class GetAllInsuranceLOBsHandler : IRequestHandler<GetAllInsuranceLOBsQuery, Result<List<GetInsuranceLOBDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllInsuranceLOBsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceLOBDTO>>> Handle(GetAllInsuranceLOBsQuery request, CancellationToken cancellationToken)
        {
            var masterClasses = await _unitOfWork.GInsuranceClass.GetAllEntitytiesAsync();
            var entities = await _unitOfWork.InsuranceLOBRepository.GetAllEntitytiesAsync();
            
            if (entities == null)
            {
                return Result<List<GetInsuranceLOBDTO>>.Failure("No Lines of Business found");
            }

            List<GetInsuranceLOBDTO> dtos = entities.Select(i => new GetInsuranceLOBDTO
            {
                Id = i.Id,
                LineOfBusiness = i.LineOfBusiness,
                Abbreviation = i.Abbreviation,
                ClassID = i.ClassID,
                ClassName = masterClasses?.FirstOrDefault(c => c.Id == i.ClassID)?.ClassName ?? "N/A",
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                UpdatedBy = i.UpdatedBy,
                UpdatedDate = i.UpdatedDate,
            }).ToList();

            return Result<List<GetInsuranceLOBDTO>>.Success(dtos);
        }
    }
}
