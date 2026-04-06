using Azure.Core;

namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Commands
{
    public record DeleteAccountCommand(int id) :IRequest<Result<bool>>;

    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAccountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(request.id);
            if (existing == null)
            {
                return Result<bool>.Failure("Account not found");
            }

            var allAccounts = await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync();
            if (allAccounts != null && allAccounts.Any(a => a.ParentId == request.id))
            {
                return Result<bool>.Failure("Cannot delete parent account with children");
            }

            bool success = await _unitOfWork.AccountNumberRepository.DeleteEntityAsync(existing.Id);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Account deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete account");
        }
    }
}
