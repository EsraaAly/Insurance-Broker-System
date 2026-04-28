using InsuranceBrokerSystem.Application.Common;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using InsuranceBrokerSystem.Application.Mediators;
using MediatR;

namespace InsuranceBrokerSystem.Application.Features.Banks.Commands.AddBank
{
    public class AddBankCommand : IManualRequest<Result<GetBankDTO>>
    {
        public AddBankDTO _addBankDTO { get; set; }
    }

    public class AddBankHandler : IManualRequestHandler<AddBankCommand, Result<GetBankDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBankHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetBankDTO>> ManualHandle(AddBankCommand request, CancellationToken cancellationToken)
        {
            // Check if bank code already exists using generic repository
            var allBanks = await _unitOfWork.GBank.GetAllEntitytiesAsync();
            var existingBank = allBanks.FirstOrDefault(b => b.Code == request._addBankDTO.Code);
            if (existingBank != null)
            {
                return Result<GetBankDTO>.Failure($"Bank with code '{request._addBankDTO.Code}' already exists");
            }

            var entity = new Domain.Entities.MasterTable.Bank
            {
                Name = request._addBankDTO.Name,
                Code = request._addBankDTO.Code,
                Description = request._addBankDTO.Description,
                SwiftCode = request._addBankDTO.SwiftCode,
                IsActive = request._addBankDTO.IsActive,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var addedEntity = await _unitOfWork.GBank.AddEntityAsync(entity);
            if (addedEntity != null)
            {
                var dto = addedEntity.Adapt<GetBankDTO>();
                await _unitOfWork.CommitAsync();
                return Result<GetBankDTO>.Success(dto, "Bank added successfully");
            }

            return Result<GetBankDTO>.Failure("Failed to add Bank");
        }
    }

    public class AddBankValidator : AbstractValidator<AddBankCommand>
    {
        public AddBankValidator()
        {
            RuleFor(x => x._addBankDTO.Name)
                .NotEmpty().WithMessage("Bank name is required")
                .MaximumLength(200).WithMessage("Bank name cannot exceed 200 characters");

            RuleFor(x => x._addBankDTO.Code)
                .NotEmpty().WithMessage("Bank code is required")
                .MaximumLength(20).WithMessage("Bank code cannot exceed 20 characters")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("Bank code can only contain alphanumeric characters");

            RuleFor(x => x._addBankDTO.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x._addBankDTO.SwiftCode)
                .MaximumLength(11).WithMessage("SWIFT code cannot exceed 11 characters")
                .Matches(@"^[A-Z0-9]{8,11}$").WithMessage("SWIFT code must be 8-11 uppercase alphanumeric characters")
                .When(x => !string.IsNullOrEmpty(x._addBankDTO.SwiftCode));
        }
    }
}
