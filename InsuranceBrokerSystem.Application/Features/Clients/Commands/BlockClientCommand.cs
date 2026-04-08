using InsuranceBrokerSystem.Application.DTOs.Client;

namespace InsuranceBrokerSystem.Application.Features.Clients.Commands
{
    public record BlockClientCommand(int Id) : IRequest<Result<string>>;

    public class BlockClientHandler : IRequestHandler<BlockClientCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlockClientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(BlockClientCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _unitOfWork.ClientRepository.GetEntityByIdAsync(request.Id);
            if (existingClient == null)
            {
                return Result<string>.Failure("Client not found");
            }

            existingClient.IsApproved = false;
            existingClient.IsRejected = false;
            existingClient.IsBlocked = true;
            existingClient.BlockedBy = "Israa";
            existingClient.BlockedDate = DateTime.UtcNow;

            var updatedClient = await _unitOfWork.ClientRepository.UpdateEntityAsync(existingClient);
            if (updatedClient == null)
            {
                return Result<string>.Failure("Failed to block client");
            }
            await _unitOfWork.CommitAsync();
            string clientDTO = updatedClient.Adapt<string>();
            return Result<string>.Success("Seccess to block client");
        }
    }

    public class BlockClientValidator : AbstractValidator<BlockClientCommand>
    {
        public BlockClientValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid client ID is required.");
        }
    }
}
