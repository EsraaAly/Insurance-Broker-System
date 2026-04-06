namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Queries
{
    public record GetAccountByIdQuery(int Id) : IRequest<Result<GetAccountDTO>>;

    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, Result<GetAccountDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAccountByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetAccountDTO>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(request.Id);
            if (accounts == null)
            {
                return Result<GetAccountDTO>.Failure("No accounts found.");
            }
            return Result<GetAccountDTO>.Success(_mapper.Map<GetAccountDTO>(accounts));
        }
    }
}
