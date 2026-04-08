using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Application.Features.Clients.Commands
{
    public record AddClientCommand(AddClientDTO dto) : IRequest<Result<GetClientDTO>>;

    public class AddClientHandler : IRequestHandler<AddClientCommand, Result<GetClientDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddClientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<GetClientDTO>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var client = request.dto.Adapt<Client>();

            Client Client = await _unitOfWork.ClientRepository.AddEntityAsync(client);
            if (Client == null)
            {
                return Result<GetClientDTO>.Failure("Failed to add client.");
            }
            await _unitOfWork.CommitAsync();
            GetClientDTO getClientDTO = Client.Adapt<GetClientDTO>();

            return Result<GetClientDTO>.Success(getClientDTO);
        }
    }
    public class AddClientValidator : AbstractValidator<AddClientCommand>
    {
        public AddClientValidator()
        {
            RuleFor(x => x.dto.ClientName).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.dto.ClientType).NotEmpty().WithMessage("Client Type must be specified.");
            RuleFor(x => x.dto.IdentityNo).NotEmpty().When(x=>x.dto.ClientType ==1).EmailAddress().WithMessage("Valid IdentityNo is required.");
            RuleFor(x => x.dto.CommercialRegistrationNo).NotEmpty().When(x => x.dto.ClientType == 2).EmailAddress().WithMessage("Commercial Registration Number is required for Corporate clients.");
            RuleFor(x => x.dto.VATNo).NotEmpty().WithMessage(errorMessage: "VAT No is required.");
            RuleFor(x => x.dto.VATNo).Length(15).WithMessage("VAT No must be 15 characters long.");

        }
    }
}
