
namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Queries.GetAllSourceOfIncomes
{
    public class GetAllSourceOfIncomesQuery : IRequest<Result<IEnumerable<GetSourceOfIncomeDTO>>>
    {
    }

    public class GetAllSourceOfIncomesHandler : IRequestHandler<GetAllSourceOfIncomesQuery, Result<IEnumerable<GetSourceOfIncomeDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSourceOfIncomesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<GetSourceOfIncomeDTO>>> Handle(GetAllSourceOfIncomesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GSourceOfIncome.GetAllEntitytiesAsync();

            var dtos = entities.Adapt<IEnumerable<GetSourceOfIncomeDTO>>();

            return Result<IEnumerable<GetSourceOfIncomeDTO>>.Success(dtos, "Source Of Incomes retrieved successfully");
        }
    }

}
