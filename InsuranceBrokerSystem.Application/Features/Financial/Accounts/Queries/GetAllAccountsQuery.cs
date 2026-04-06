namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Queries
{
    public record GetAllAccountsQuery : IRequest<Result<List<GetAccountDTO>>>;

    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, Result<List<GetAccountDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllAccountsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAccountDTO>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync();
            if (accounts == null || accounts.Count == 0)
            {
                return Result<List<GetAccountDTO>>.Failure("No accounts found.");
            }
            return Result<List<GetAccountDTO>>.Success(_mapper.Map<List<GetAccountDTO>>(accounts));
        }
    }
}
