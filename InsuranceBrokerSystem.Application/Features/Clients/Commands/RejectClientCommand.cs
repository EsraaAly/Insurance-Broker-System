using InsuranceBrokerSystem.Application.DTOs.Client;

namespace InsuranceBrokerSystem.Application.Features.Clients.Commands
{
    public record RejectClientCommand(int Id) : IRequest<Result<string>>;

    public class RejectClientHandler : IRequestHandler<RejectClientCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RejectClientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(RejectClientCommand request, CancellationToken cancellationToken)
        {
            
            var existingClient = await _unitOfWork.ClientRepository.GetEntityByIdAsync(request.Id);
            if (existingClient == null)
            {
                return Result<string>.Failure("Client not found");
            }

            existingClient.IsApproved = false;
            existingClient.IsRejected = true;
            existingClient.IsBlocked = false;
            existingClient.RejectedBy = "Israa";
            existingClient.RejectedDate = DateTime.UtcNow;

            var updatedClient = await _unitOfWork.ClientRepository.UpdateEntityAsync(existingClient);
            if (updatedClient == null)
            {
                return Result<string>.Failure("Failed to reject client");
            }
            await _unitOfWork.CommitAsync();
            GetClientDTO clientDTO = updatedClient.Adapt<GetClientDTO>();
            return Result<string>.Success("Success to reject client");
        }
    }

    public class RejectClientValidator : AbstractValidator<RejectClientCommand>
    {
        public RejectClientValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid client ID is required.");
        }
    }
}
