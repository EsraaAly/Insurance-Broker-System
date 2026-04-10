namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class PolicyTypeController : ControllerBase
    {
        private readonly ISender _mediator;

        public PolicyTypeController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.PolicyType.AddPolicyType)]
        public async Task<IActionResult> AddPolicyTypeAsync(AddPolicyTypeCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.PolicyType.GetAllPolicyTypes)]
        public async Task<IActionResult> GetAllPolicyTypesAsync()
        {
            var result = await _mediator.Send(new GetAllPolicyTypesQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.PolicyType.GetPolicyTypeById+"/{id}")]
        public async Task<IActionResult> GetPolicyTypeByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetPolicyTypeByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.PolicyType.UpdatePolicyType)]
        public async Task<IActionResult> UpdatePolicyTypeAsync(UpdatePolicyTypeCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.PolicyType.DeletePolicyType+"{id}")]
        public async Task<IActionResult> DeletePolicyTypeAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeletePolicyTypeCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
