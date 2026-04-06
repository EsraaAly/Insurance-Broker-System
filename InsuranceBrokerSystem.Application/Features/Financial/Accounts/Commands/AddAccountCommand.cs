namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Commands
{
    public record AddAccountCommand(CreateAccountDTO dto):IRequest<Result<GetAccountDTO>>;

    public class AddAccountHandler : IRequestHandler<AddAccountCommand, Result<GetAccountDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetAccountDTO>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            if (request.dto == null) return Result<GetAccountDTO>.Failure("Account data is null");

            var existingDeleted = await _unitOfWork.AccountNumberRepository.GetByExpressionAsync(x => x.AccountName == request.dto.AccountName && x.IsDeleted == true);

            if (existingDeleted != null)
            {
                existingDeleted.IsDeleted = false;
                existingDeleted.UpdatedDate = DateTime.Now;

                await _unitOfWork.AccountNumberRepository.UpdateEntityAsync(existingDeleted);
                await _unitOfWork.CommitAsync();
                return Result<GetAccountDTO>.Success(_mapper.Map<GetAccountDTO>(existingDeleted), "Account restored successfully");
            }
            Account acc = _mapper.Map<Account>(request.dto);

            var parent = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(request.dto.ParentId ?? 0);
            var siblingsResponse = parent != null ? await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync() : null;
            var siblings = siblingsResponse != null ? siblingsResponse.Where(s => s.ParentId == parent!.Id).ToList() : null;

            string newAccountNumber = await _unitOfWork.AccountNumberRepository.GenerateAsync(parent, siblings, (int)request.dto.AccountType);

            acc.AccountNumber = newAccountNumber;
            acc.CreatedBy = "Israa";

            if (request.dto.ParentId == null)
            {
                acc.Level = 1;
            }
            else
            {
                acc.Level = request.dto.Level + 1;
            }

            acc = await _unitOfWork.AccountNumberRepository.AddEntityAsync(acc);
            await _unitOfWork.CommitAsync();

            GetAccountDTO accDTO = _mapper.Map<GetAccountDTO>(acc);
            return Result<GetAccountDTO>.Success(accDTO, "Account created successfully");
        }
    }
}
