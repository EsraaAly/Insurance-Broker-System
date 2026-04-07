
namespace InsuranceBrokerSystem.UI.Services.Financial
{
    public class ChartOfAccountApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44314")
        };
        public ChartOfAccountApiService() { }

        public async Task<bool> AddAccountAsync(Account account)
        {
            if (account == null) return false;

            var dto = MapUIToCreateDto(account);
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Financial.Account.AddAccount, dto);
            
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Account created successfully!");
                return true;
            }
            
            MessageBox.Show("Error: Could not create account.");
            return false;
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            if (account == null || account.Id == 0) return false;

            var dto = MapUIToEditDto(account);
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Financial.Account.UpdateAccount, dto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Account updated successfully!");
                return true;
            }

            MessageBox.Show("Error: Could not update account.");
            return false;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiRoutes.Financial.Account.DeleteAccount}/{id}");
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Account deleted successfully!");
                return true;
            }

            MessageBox.Show("Error: Could not delete account.");
            return false;
        }

        public async Task<List<Account>> LoadAccountsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<GetAccountDTO>>>(ApiRoutes.Financial.Account.GetAllAccounts);

                if (response != null && response.Succeeded)
                {
                    return response.Data.Select(dto => MapDtoToUIModel(dto)).ToList();
                }
                return new List<Account>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection Error: {ex.Message}");
                return new List<Account>();
            }
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
