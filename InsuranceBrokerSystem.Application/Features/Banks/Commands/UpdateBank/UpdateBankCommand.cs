using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Banks.Commands.UpdateBank
{
    public class UpdateBankCommand : IRequest<Result<GetBankDTO>>
    {
        public UpdateBankDTO _updateBankDTO { get; set; }
    }

    public class UpdateBankHandler : IRequestHandler<UpdateBankCommand, Result<GetBankDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBankHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetBankDTO>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
            var existingBank = await _unitOfWork.GBank.GetEntityByIdAsync(request._updateBankDTO.Id);
            if (existingBank == null)
            {
                return Result<GetBankDTO>.Failure("Bank not found");
            }

            // Check if code conflicts with another bank using generic repository
            var allBanks = await _unitOfWork.GBank.GetAllEntitytiesAsync();
            var bankWithSameCode = allBanks.FirstOrDefault(b => b.Code == request._updateBankDTO.Code && b.Id != request._updateBankDTO.Id);
            if (bankWithSameCode != null)
            {
                return Result<GetBankDTO>.Failure($"Bank with code '{request._updateBankDTO.Code}' already exists");
            }

            existingBank.Name = request._updateBankDTO.Name;
            existingBank.Code = request._updateBankDTO.Code;
            existingBank.Description = request._updateBankDTO.Description;
            existingBank.SwiftCode = request._updateBankDTO.SwiftCode;
            existingBank.IsActive = request._updateBankDTO.IsActive;
            existingBank.UpdatedBy = "System";
            existingBank.UpdatedDate = DateTime.UtcNow;

            var updatedEntity = await _unitOfWork.GBank.UpdateEntityAsync(existingBank);
            if (updatedEntity != null)
            {
                var dto = updatedEntity.Adapt<GetBankDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetBankDTO>.Success(dto, "Bank updated successfully");
            }

            return Result<GetBankDTO>.Failure("Failed to update Bank");
        }
    }

    public class UpdateBankValidator : AbstractValidator<UpdateBankCommand>
    {
        public UpdateBankValidator()
        {
            RuleFor(x => x._updateBankDTO.Id)
                .GreaterThan(0).WithMessage("Valid bank ID is required");

            RuleFor(x => x._updateBankDTO.Name)
                .NotEmpty().WithMessage("Bank name is required")
                .MaximumLength(200).WithMessage("Bank name cannot exceed 200 characters");

            RuleFor(x => x._updateBankDTO.Code)
                .NotEmpty().WithMessage("Bank code is required")
                .MaximumLength(20).WithMessage("Bank code cannot exceed 20 characters")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("Bank code can only contain alphanumeric characters");

            RuleFor(x => x._updateBankDTO.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x._updateBankDTO.SwiftCode)
                .MaximumLength(11).WithMessage("SWIFT code cannot exceed 11 characters")
                .Matches(@"^[A-Z0-9]{8,11}$").WithMessage("SWIFT code must be 8-11 uppercase alphanumeric characters")
                .When(x => !string.IsNullOrEmpty(x._updateBankDTO.SwiftCode));
        }
    }
}
