namespace InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Queries.GetInsuranceLOBById
{
    public class GetInsuranceLOBByIdQuery : IRequest<Result<GetInsuranceLOBDTO>>
    {
        public int Id { get; set; }
    }

    public class GetInsuranceLOBByIdHandler : IRequestHandler<GetInsuranceLOBByIdQuery, Result<GetInsuranceLOBDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceLOBByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceLOBDTO>> Handle(GetInsuranceLOBByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.InsuranceLOBRepository.GetEntityByIdAsync(request.Id);

            if (entity == null)
            {
                return Result<GetInsuranceLOBDTO>.Failure("Line of Business not found");
            }

            GetInsuranceLOBDTO dto = new GetInsuranceLOBDTO
            {
                Id = entity.Id,
                LineOfBusiness = entity.LineOfBusiness,
                Abbreviation = entity.Abbreviation,
                ClassID = entity.ClassID,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };

            return Result<GetInsuranceLOBDTO>.Success(dto);
        }
    }
}
