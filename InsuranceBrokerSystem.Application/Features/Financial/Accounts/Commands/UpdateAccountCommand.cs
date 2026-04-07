namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Commands
{
    public record UpdateAccountCommand(EditAccountDTO dto):IRequest<Result<GetAccountDTO>>;

    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Result<GetAccountDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAccountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<GetAccountDTO>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(request.dto.Id);
            if (existing == null)
            {
                return Result<GetAccountDTO>.Failure("Account not found");
            }

            request.dto.Adapt(existing);
            bool success = await _unitOfWork.AccountNumberRepository.UpdateEntityAsync(existing);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetAccountDTO>.Success(existing.Adapt<GetAccountDTO>(), "Account updated successfully");
            }

            return Result<GetAccountDTO>.Failure("Failed to update account");
        }
    }
}
