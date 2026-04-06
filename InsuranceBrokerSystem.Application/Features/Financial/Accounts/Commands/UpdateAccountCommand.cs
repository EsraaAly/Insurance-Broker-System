namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Commands
{
    public record UpdateAccountCommand(EditAccountDTO dto):IRequest<Result<GetAccountDTO>>;

    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Result<GetAccountDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetAccountDTO>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(request.dto.Id);
            if (existing == null)
            {
                return Result<GetAccountDTO>.Failure("Account not found");
            }

            _mapper.Map(request.dto, existing);
            bool success = await _unitOfWork.AccountNumberRepository.UpdateEntityAsync(existing);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetAccountDTO>.Success(_mapper.Map<GetAccountDTO>(existing), "Account updated successfully");
            }

            return Result<GetAccountDTO>.Failure("Failed to update account");
        }
    }
}
