

using InsuranceBrokerSystem.Application.Features.Financial.Accounts.Commands;

namespace InsuranceBrokerSystem.Api.Controllers.Financial
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Financial.Account.GetAllAccounts)]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var result = await _mediator.Send(new GetAllAccountsQuery());
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.Financial.Account.UpdateAccount)]
        public async Task<IActionResult> UpdateAccountAsync(EditAccountDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var result = await _mediator.Send(new UpdateAccountCommand(dto));
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.Financial.Account.DeleteAccount + "{id}")]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            var result = await _mediator.Send(new DeleteAccountCommand(id));
            return result.ToActionResult();
        }

        [HttpPost(ApiRoutes.Financial.Account.AddAccount)]
        public async Task<IActionResult> AddAccountAsync([FromBody] CreateAccountDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var result = await _mediator.Send(new AddAccountCommand(dto));
            return result.ToActionResult();
        }
    }
}
