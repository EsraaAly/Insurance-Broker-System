using InsuranceBrokerSystem.Application.DTOs.Master_Table.PolicyType;

namespace InsuranceBrokerSystem.Application.Features.PolicyTypes.Queries.GetPolicyTypeById
{
    public class GetPolicyTypeByIdQuery : IRequest<Result<GetPolicyTypeDTO>>
    {
        public int Id { get; set; }
    }

    public class GetPolicyTypeByIdHandler : IRequestHandler<GetPolicyTypeByIdQuery, Result<GetPolicyTypeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPolicyTypeByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPolicyTypeDTO>> Handle(GetPolicyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GPolicyType.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<GetPolicyTypeDTO>.Failure("Policy Type not found");
            }

            var dto = entity.Adapt<GetPolicyTypeDTO>();

            return Result<GetPolicyTypeDTO>.Success(dto, "Policy Type retrieved successfully");
        }
    }
    class GetPolicyTypeByIdValidator : AbstractValidator<GetPolicyTypeByIdQuery>
    {
        public GetPolicyTypeByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }

}
