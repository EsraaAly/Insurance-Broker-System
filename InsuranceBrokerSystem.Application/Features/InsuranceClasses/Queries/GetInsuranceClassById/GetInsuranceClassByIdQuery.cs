namespace InsuranceBrokerSystem.Application.Features.InsuranceClasses.Queries.GetInsuranceClassById
{
    public class GetInsuranceClassByIdQuery : IRequest<Result<GetInsuranceClassDTO>>
    {
        public int Id { get; set; }
    }

    public class GetInsuranceClassByIdHandler : IRequestHandler<GetInsuranceClassByIdQuery, Result<GetInsuranceClassDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInsuranceClassByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInsuranceClassDTO>> Handle(GetInsuranceClassByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(request.Id);

            if (entity == null)
            {
                return Result<GetInsuranceClassDTO>.Failure("Insurance Class not found");
            }

            var dto = new GetInsuranceClassDTO
            {
                Id = entity.Id,
                ClassName = entity.ClassName,
                Abbreviation = entity.Abbreviation,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };

            return Result<GetInsuranceClassDTO>.Success(dto);
        }
    }
}
