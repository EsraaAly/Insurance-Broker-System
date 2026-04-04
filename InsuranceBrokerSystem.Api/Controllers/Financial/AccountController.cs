
namespace InsuranceBrokerSystem.Api.Controllers.Financial
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet(ApiRoutes.Financial.Account.GetAllAccounts)]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var result = await _accountService.GetAllAccountsAsync();
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.Financial.Account.UpdateAccount)]
        public async Task<IActionResult> UpdateAccountAsync(EditAccountDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var result = await _accountService.UpdateAccountAsync(dto);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.Financial.Account.DeleteAccount + "/{id}")]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            return result.ToActionResult();
        }

        [HttpPost(ApiRoutes.Financial.Account.AddAccount)]
        public async Task<IActionResult> AddAccountAsync([FromBody] CreateAccountDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var result = await _accountService.AddAccountAsync(dto);
            return result.ToActionResult();
        }
    }
}
