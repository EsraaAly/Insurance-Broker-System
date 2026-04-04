
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
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpPut(ApiRoutes.Financial.Account.UpdateAccount)]
        public async Task<IActionResult> UpdateAccountAsync(EditAccountDTO dto)
        {
            if (dto == null) return BadRequest();
            var result = await _accountService.UpdateAccountAsync(dto);
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Financial.Account.DeleteAccount + "/{id}")]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (!result) return BadRequest("Cannot delete account. Ensure it has no children.");
            return Ok();
        }

        [HttpPost(ApiRoutes.Financial.Account.AddAccount)]
        public async Task<IActionResult> AddAccountAsync([FromBody]CreateAccountDTO dto)
        {
            if (dto == null) 
            { 
                return BadRequest(); 
            }

            GetAccountDTO accDTO = await _accountService.AddAccountAsync(dto);
            return Ok(accDTO);
        }
    }
}
