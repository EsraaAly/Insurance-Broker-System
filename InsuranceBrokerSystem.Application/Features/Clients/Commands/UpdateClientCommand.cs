using InsuranceBrokerSystem.Application.DTOs.Client;

namespace InsuranceBrokerSystem.Application.Features.Clients.Commands
{
    public record UpdateClientCommand(UpdateClientDTO dto) : IRequest<Result<GetClientDTO>>;

    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, Result<GetClientDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateClientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetClientDTO>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var existingClient = await _unitOfWork.ClientRepository.GetEntityByIdAsync(request.dto.Id);
            if (existingClient == null)
            {
                return Result<GetClientDTO>.Failure("Client not found");
            }

            var updatedClient = request.dto.Adapt<Client>();
            
            var isSuccess = await _unitOfWork.ClientRepository.UpdateEntityAsync(updatedClient);
            if (!isSuccess)
            {
                return Result<GetClientDTO>.Failure("Failed to update client.");
            }
            await _unitOfWork.CommitAsync();
            GetClientDTO getClientDTO = updatedClient.Adapt<GetClientDTO>();
            return Result<GetClientDTO>.Success(getClientDTO);
        }
    }

    public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientValidator()
        {
            RuleFor(x => x.dto.Id).GreaterThan(0).WithMessage("Valid client ID is required.");
            RuleFor(x => x.dto.ClientName).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.dto.ClientType).NotEmpty().WithMessage("Client Type must be specified.");
            RuleFor(x => x.dto.IdentityNo).NotEmpty().When(x => x.dto.ClientType == 1).WithMessage("IdentityNo is required for individual clients.");
            RuleFor(x => x.dto.CommercialRegistrationNo).NotEmpty().When(x => x.dto.ClientType == 2).WithMessage("Commercial Registration Number is required for Corporate clients.");
            RuleFor(x => x.dto.VATNo).NotEmpty().WithMessage("VAT No is required.");
            RuleFor(x => x.dto.VATNo).Length(15).WithMessage("VAT No must be 15 characters long.");
        }
    }
}
