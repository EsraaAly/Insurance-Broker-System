
using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Financial
{
    public class ChartOfAccountApiService
    {
        private readonly HttpClientService _httpClientService;

        public ChartOfAccountApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<bool> AddAccountAsync(Account account)
        {
            if (account == null) return false;

            var dto = MapUIToCreateDto(account);
            var response = await _httpClientService.PostAsync<string, object>(ApiRoutes.Financial.Account.AddAccount, dto);
            return response.Successed;
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            if (account == null || account.Id == 0) return false;

            var dto = MapUIToEditDto(account);
            var response = await _httpClientService.PutAsync<string, object>(ApiRoutes.Financial.Account.UpdateAccount, dto);
            return response.Successed;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var response = await _httpClientService.DeleteAsync($"{ApiRoutes.Financial.Account.DeleteAccount}/{id}");
            return response.Successed;
        }

        public async Task<List<Account>> LoadAccountsAsync()
        {
            var response = await _httpClientService.GetAsync<List<GetAccountDTO>>(ApiRoutes.Financial.Account.GetAllAccounts);
            
            if (response.Successed && response.Data != null)
            {
                return response.Data.Select(dto => MapDtoToUIModel(dto)).ToList();
            }
            return new List<Account>();
        }

        private CreateAccountDTO MapUIToCreateDto(Account account)
        {
            return new CreateAccountDTO
            {
                AccountName = account.AccountName,
                Description = account.Description,
                Level = account.Level,
                AccountType = account.Type,
                IsPostable = account.IsPostable,
                ParentId = account.ParentId,
                Children = account.Children.Select(c => MapUIToCreateDto(c)).ToList()
            };
        }

        private EditAccountDTO MapUIToEditDto(Account account)
        {
            return new EditAccountDTO
            {
                Id = account.Id,
                AccountName = account.AccountName,
                Description = account.Description,
                IsPostable = account.IsPostable
            };
        }

        private Account MapDtoToUIModel(GetAccountDTO dto, string parentName = null)
        {

            var account = new Account
            {
                Id = dto.Id,
                AccountNumber = dto.AccountNumber,
                AccountName = dto.AccountName,
                Description = dto.Description,
                Level = dto.Level,
                ParentId = dto.ParentId,
                ParentName = parentName ?? "No Parent",
                Type = dto.AccountType,
                IsPostable = dto.IsPostable
            };

            if (dto.Children != null && dto.Children.Any())
            {
                foreach (var childDto in dto.Children)
                {
                    // تأكد إن الابن مش هو نفسه الأب عشان متدخلش في Infinite Loop
                    account.Children.Add(MapDtoToUIModel(childDto, account.AccountName));
                }
            }
            return account;
        }


    }
}
