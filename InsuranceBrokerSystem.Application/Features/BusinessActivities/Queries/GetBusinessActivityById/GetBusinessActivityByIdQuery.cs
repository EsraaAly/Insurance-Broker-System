using InsuranceBrokerSystem.Application.DTOs.Master_Table.BusinessActivity;

namespace InsuranceBrokerSystem.Application.Features.BusinessActivities.Queries.GetBusinessActivityById
{
    public class GetBusinessActivityByIdQuery : IRequest<Result<GetBusinessActivityDTO>>
    {
        public int Id { get; set; }
    }

    public class GetBusinessActivityByIdHandler : IRequestHandler<GetBusinessActivityByIdQuery, Result<GetBusinessActivityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBusinessActivityByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetBusinessActivityDTO>> Handle(GetBusinessActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GBusinessActivity.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<GetBusinessActivityDTO>.Failure("Business Activity not found");
            }

            var dto = entity.Adapt<GetBusinessActivityDTO>();

            return Result<GetBusinessActivityDTO>.Success(dto, "Business Activity retrieved successfully");
        }
    }
    public class GetBusinessActivityByIdValidator : AbstractValidator<GetBusinessActivityByIdQuery>
    {
        public GetBusinessActivityByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

}
