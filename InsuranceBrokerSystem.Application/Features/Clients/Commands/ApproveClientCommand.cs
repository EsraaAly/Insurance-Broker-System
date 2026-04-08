using InsuranceBrokerSystem.Application.DTOs.Client;

namespace InsuranceBrokerSystem.Application.Features.Clients.Commands
{
    public record ApproveClientCommand(int Id, ApproveClientDTO dto) : IRequest<Result<GetClientDTO>>;

    public class ApproveClientHandler : IRequestHandler<ApproveClientCommand, Result<GetClientDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApproveClientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetClientDTO>> Handle(ApproveClientCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _unitOfWork.ClientRepository.GetEntityByIdAsync(request.Id);
            if (existingClient == null)
            {
                return Result<GetClientDTO>.Failure("Client not found");
            }

            existingClient.IsApproved = true;
            existingClient.IsRejected = false;
            existingClient.IsBlocked = false;
            existingClient.ApprovedBy = request.dto.ApprovedBy;
            existingClient.ApprovedDate = request.dto.ApprovedDate ?? DateTime.UtcNow;
            existingClient.AccountPremium = request.dto.AccountPremium;

            var updatedClient = await _unitOfWork.ClientRepository.UpdateEntityAsync(existingClient);
            if (updatedClient == null)
            {
                return Result<GetClientDTO>.Failure("Failed to approve client");
            }

            GetClientDTO clientDTO = updatedClient.Adapt<GetClientDTO>();
            return Result<GetClientDTO>.Success(clientDTO);
        }
    }

    public class ApproveClientValidator : AbstractValidator<ApproveClientCommand>
    {
        public ApproveClientValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid client ID is required.");
            RuleFor(x => x.dto.ApprovedBy).NotEmpty().WithMessage("Approved by is required.");
        }
    }
}
