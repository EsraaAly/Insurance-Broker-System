namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Queries.GetSourceOfIncomeById
{
    public class GetSourceOfIncomeByIdQuery : IRequest<Result<GetSourceOfIncomeDTO>>
    {
        public int Id { get; set; }
    }

    public class GetSourceOfIncomeByIdHandler : IRequestHandler<GetSourceOfIncomeByIdQuery, Result<GetSourceOfIncomeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSourceOfIncomeByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetSourceOfIncomeDTO>> Handle(GetSourceOfIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GSourceOfIncome.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<GetSourceOfIncomeDTO>.Failure("Source Of Income not found");
            }

            var dto = entity.Adapt<GetSourceOfIncomeDTO>();

            return Result<GetSourceOfIncomeDTO>.Success(dto, "Source Of Income retrieved successfully");
        }
    }
    public class GetSourceOfIncomeByIdValidator : AbstractValidator<GetSourceOfIncomeByIdQuery>
    {
        public GetSourceOfIncomeByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }


}
