using Mapster;

namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Queries
{
    public record GetAllAccountsQuery : IRequest<Result<List<GetAccountDTO>>>;

    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, Result<List<GetAccountDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        public GetAllAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
        }
        public async Task<Result<List<GetAccountDTO>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync(a => a.Children);

            if (accounts == null || !accounts.Any())
            {
                return Result<List<GetAccountDTO>>.Failure("No accounts found.");
            }

            var dtos = accounts.Adapt<List<GetAccountDTO>>();
            return Result<List<GetAccountDTO>>.Success(dtos);
        }
    }
}
