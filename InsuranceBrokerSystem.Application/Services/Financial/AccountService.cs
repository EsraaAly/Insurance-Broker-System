

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

        public async Task<GetAccountDTO> AddAccountAsync(CreateAccountDTO dto)
        {
            var existingDeleted = await _unitOfWork.AccountNumber.GetByExpressionAsync(x => x.AccountName == dto.AccountName && x.IsDeleted == true);

            if (existingDeleted != null)
            {
                // RESTORE: Instead of adding, just reactivate the old one
                existingDeleted.IsDeleted = false;
                existingDeleted.UpdatedDate = DateTime.Now;
                // Update other fields from dto if necessary...

                await _unitOfWork.AccountNumber.UpdateEntityAsync(existingDeleted);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<GetAccountDTO>(existingDeleted);
            }
            Account acc = _mapper.Map<Account>(dto);
            var parent = await _unitOfWork.AccountNumber.GetEntityByIdAsync(dto.ParentId ?? 0);
            var siblings = parent != null? await _unitOfWork.AccountNumber.GetAllEntitytiesAsync() :null;
            if (siblings != null)
                siblings  = siblings.Where(s => s.ParentId == parent.Id).ToList();
            string newAccountNumber = await _unitOfWork.AccountNumber.GenerateAsync(parent, siblings,(int)dto.AccountType);

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
            acc = await _unitOfWork.AccountNumber.AddEntityAsync(acc);

            await _unitOfWork.CommitAsync();

            GetAccountDTO accDTO = _mapper.Map<GetAccountDTO>(acc);
            return accDTO;
        }

        public Task<GetAccountDTO> GetAccountByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAccountDTO> UpdateAccountAsync(EditAccountDTO dto)
        {
            var existing = await _unitOfWork.AccountNumber.GetEntityByIdAsync(dto.Id);
            if (existing == null) throw new KeyNotFoundException("Account not found");

            _mapper.Map(dto, existing);
            await _unitOfWork.AccountNumber.UpdateEntityAsync(existing);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GetAccountDTO>(existing);
        }

        public async Task<List<GetAccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountNumber.GetAllEntitytiesAsync();
            return _mapper.Map<List<GetAccountDTO>>(accounts);
        }

        public async Task<List<GetAccountDTO>> GetRootAccountsAsync()
        {
            var allAccounts = await GetAllAccountsAsync();
            // Return only Level 1 accounts (Roots)
            return allAccounts.Where(a => a.Level == 1 || a.ParentId == null).ToList();
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var existing = await _unitOfWork.AccountNumber.GetEntityByIdAsync(id);
            if (existing == null) return false;

            var allAccounts = await _unitOfWork.AccountNumber.GetAllEntitytiesAsync();
            if (allAccounts.Any(a => a.ParentId == id))
            {
                return false; // Cannot delete parent with children
            }

            // A real app might do _unitOfWork.AccountNumber.DeleteEntityAsync(existing)
            // Assuming DeleteEntityAsync exists. Let's try DeleteEntityAsync.
            await _unitOfWork.AccountNumber.DeleteEntityAsync(existing.Id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        //public async Task<GetAccountDTO> GetAccountByIdAsync(int id)
        //{
        //    var account = await _unitOfWork.AccountNumber.GetEntityByIdAsync(id);
        //    return _mapper.Map<GetAccountDTO>(account);
        //}
    }
}
