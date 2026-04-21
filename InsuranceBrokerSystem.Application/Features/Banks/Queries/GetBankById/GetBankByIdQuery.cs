using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using MediatR;
using InsuranceBrokerSystem.Application.Common;

namespace InsuranceBrokerSystem.Application.Features.Banks.Queries.GetBankById
{
    public class GetBankByIdQuery : IRequest<Result<GetBankDTO>>
    {
        public int Id { get; set; }
    }

    public class GetBankByIdHandler : IRequestHandler<GetBankByIdQuery, Result<GetBankDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBankByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetBankDTO>> Handle(GetBankByIdQuery request, CancellationToken cancellationToken)
        {
            var bank = await _unitOfWork.GBank.GetEntityByIdAsync(request.Id);
            if (bank == null)
            {
                return Result<GetBankDTO>.Failure("Bank not found");
            }

            var dto = bank.Adapt<GetBankDTO>();
            return Result<GetBankDTO>.Success(dto, "Bank retrieved successfully");
        }
    }

    public class GetBankByIdValidator : AbstractValidator<GetBankByIdQuery>
    {
        public GetBankByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Valid bank ID is required");
        }
    }
}
