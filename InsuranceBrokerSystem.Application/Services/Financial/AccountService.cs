

namespace InsuranceBrokerSystem.Application.Services.Financial
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetAccountDTO>> AddAccountAsync(CreateAccountDTO dto)
        {
            if (dto == null) return Result<GetAccountDTO>.Failure("Account data is null");

            var existingDeleted = await _unitOfWork.AccountNumberRepository.GetByExpressionAsync(x => x.AccountName == dto.AccountName && x.IsDeleted == true);

            if (existingDeleted != null)
            {
                existingDeleted.IsDeleted = false;
                existingDeleted.UpdatedDate = DateTime.Now;

                await _unitOfWork.AccountNumberRepository.UpdateEntityAsync(existingDeleted);
                await _unitOfWork.CommitAsync();
                return Result<GetAccountDTO>.Success(_mapper.Map<GetAccountDTO>(existingDeleted), "Account restored successfully");
            }

            Account acc = _mapper.Map<Account>(dto);
            var parent = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(dto.ParentId ?? 0);
            var siblingsResponse = parent != null ? await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync() : null;
            var siblings = siblingsResponse != null ? siblingsResponse.Where(s => s.ParentId == parent!.Id).ToList() : null;
            
            string newAccountNumber = await _unitOfWork.AccountNumberRepository.GenerateAsync(parent, siblings, (int)dto.AccountType);

            acc.AccountNumber = newAccountNumber;
            acc.CreatedBy = "Israa";

            if (dto.ParentId == null)
            {
                acc.Level = 1;
            }
            else
            {
                acc.Level = dto.Level + 1;
            }

            acc = await _unitOfWork.AccountNumberRepository.AddEntityAsync(acc);
            await _unitOfWork.CommitAsync();

            GetAccountDTO accDTO = _mapper.Map<GetAccountDTO>(acc);
            return Result<GetAccountDTO>.Success(accDTO, "Account created successfully");
        }

        public async Task<Result<GetAccountDTO>> GetAccountByIdAsync(int id)
        {
            var entity = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return Result<GetAccountDTO>.Failure("Account not found");
            }
            return Result<GetAccountDTO>.Success(_mapper.Map<GetAccountDTO>(entity));
        }

        public async Task<Result<GetAccountDTO>> UpdateAccountAsync(EditAccountDTO dto)
        {
            var existing = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(dto.Id);
            if (existing == null)
            {
                return Result<GetAccountDTO>.Failure("Account not found");
            }

            _mapper.Map(dto, existing);
            bool success = await _unitOfWork.AccountNumberRepository.UpdateEntityAsync(existing);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetAccountDTO>.Success(_mapper.Map<GetAccountDTO>(existing), "Account updated successfully");
            }

            return Result<GetAccountDTO>.Failure("Failed to update account");
        }

        public async Task<Result<List<GetAccountDTO>>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync();
            if (accounts == null)
            {
                return Result<List<GetAccountDTO>>.Failure("No accounts found");
            }
            return Result<List<GetAccountDTO>>.Success(_mapper.Map<List<GetAccountDTO>>(accounts));
        }

        public async Task<Result<List<GetAccountDTO>>> GetRootAccountsAsync()
        {
            var result = await GetAllAccountsAsync();
            if (!result.Succeeded || result.Data == null)
            {
                return result;
            }
            var rootAccounts = result.Data.Where(a => a.Level == 1 || a.ParentId == null).ToList();
            return Result<List<GetAccountDTO>>.Success(rootAccounts);
        }

        public async Task<Result<bool>> DeleteAccountAsync(int id)
        {
            var existing = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(id);
            if (existing == null)
            {
                return Result<bool>.Failure("Account not found");
            }

            var allAccounts = await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync();
            if (allAccounts != null && allAccounts.Any(a => a.ParentId == id))
            {
                return Result<bool>.Failure("Cannot delete parent account with children");
            }

            bool success = await _unitOfWork.AccountNumberRepository.DeleteEntityAsync(existing.Id);
            if (success)
            {
                await _unitOfWork.CommitAsync();
                return Result<bool>.Success(true, "Account deleted successfully");
            }

            return Result<bool>.Failure("Failed to delete account");
        }

        //public async Task<GetAccountDTO> GetAccountByIdAsync(int id)
        //{
        //    var account = await _unitOfWork.AccountNumberRepository.GetEntityByIdAsync(id);
        //    return _mapper.Map<GetAccountDTO>(account);
        //}
    }
}
