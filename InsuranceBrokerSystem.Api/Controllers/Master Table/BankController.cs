using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using InsuranceBrokerSystem.Application.Features.Banks.Commands.AddBank;
using InsuranceBrokerSystem.Application.Features.Banks.Commands.UpdateBank;
using InsuranceBrokerSystem.Application.Features.Banks.Commands.DeleteBank;
using InsuranceBrokerSystem.Application.Features.Banks.Queries.GetAllBanks;
using InsuranceBrokerSystem.Application.Features.Banks.Queries.GetBankById;

namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly ISender _mediator;

        public BankController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.Bank.AddBank)]
        public async Task<IActionResult> AddBankAsync(AddBankDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new AddBankCommand { _addBankDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Bank.GetAllBanks)]
        public async Task<IActionResult> GetAllBanksAsync()
        {
            var result = await _mediator.Send(new GetAllBanksQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Bank.GetBankById+"/{id}")]
        public async Task<IActionResult> GetBankByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetBankByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.Bank.UpdateBank)]
        public async Task<IActionResult> UpdateBankAsync(UpdateBankDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new UpdateBankCommand { _updateBankDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.Bank.DeleteBank+"/{id}")]
        public async Task<IActionResult> DeleteBankAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteBankCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
