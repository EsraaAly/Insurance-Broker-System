namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class SourceOfIncomeController : ControllerBase
    {
        private readonly ISender _mediator;

        public SourceOfIncomeController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.SourceOfIncome.AddSourceOfIncome)]
        public async Task<IActionResult> AddSourceOfIncomeAsync(AddSourceOfIncomeCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.SourceOfIncome.GetAllSourceOfIncomes)]
        public async Task<IActionResult> GetAllSourceOfIncomesAsync()
        {
            var result = await _mediator.Send(new GetAllSourceOfIncomesQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.SourceOfIncome.GetSourceOfIncomeById+"/{id}")]
        public async Task<IActionResult> GetSourceOfIncomeByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetSourceOfIncomeByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.SourceOfIncome.UpdateSourceOfIncome)]
        public async Task<IActionResult> UpdateSourceOfIncomeAsync(UpdateSourceOfIncomeCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.SourceOfIncome.DeleteSourceOfIncome+"{id}")]
        public async Task<IActionResult> DeleteSourceOfIncomeAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteSourceOfIncomeCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
