namespace InsuranceBrokerSystem.Application.Features.Clients.Commands
{
    public record DeleteClientCommand(int Id) : IRequest<Result<string>>;

    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteClientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _unitOfWork.ClientRepository.GetEntityByIdAsync(request.Id);
            if (existingClient == null)
            {
                return Result<string>.Failure("Client not found");
            }

            var result = await _unitOfWork.ClientRepository.DeleteEntityAsync(request.Id);
            if (!result)
            {
                return Result<string>.Failure("Failed to delete client");
            }
            await _unitOfWork.CommitAsync();
            return Result<string>.Success("Client deleted successfully");
        }
    }

    public class DeleteClientValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid client ID is required.");
        }
    }
}
