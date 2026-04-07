namespace InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Queries.GetInsuranceLOBByClassId
{
    public class GetInsuranceLOBByClassIdQuery : IRequest<Result<List<GetInsuranceLOBDTO>>>
    {
        public int ClassId { get; set; }
    }

    public class GetInsuranceLOBByClassIdHandler : IRequestHandler<GetInsuranceLOBByClassIdQuery, Result<List<GetInsuranceLOBDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceLOBByClassIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetInsuranceLOBDTO>>> Handle(GetInsuranceLOBByClassIdQuery request, CancellationToken cancellationToken)
        {
            var LOB_List = await _unitOfWork.InsuranceLOBRepository.GetInsuranceLOBByClassIdAsync(request.ClassId);
            if (LOB_List == null)
            {
                return Result<List<GetInsuranceLOBDTO>>.Failure("No Lines of Business found for this class");
            }
            List<GetInsuranceLOBDTO> dto = LOB_List.Adapt<List<GetInsuranceLOBDTO>>();
            return Result<List<GetInsuranceLOBDTO>>.Success(dto);
        }
    }
}
