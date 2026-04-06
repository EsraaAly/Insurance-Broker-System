
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceClassController : ControllerBase
    {
        private readonly ISender _mediator;

        public InsuranceClassController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceClass.GetAllInsuranceClasses)]
        public async Task<IActionResult> GetAllClassesAsync()
        {
            var result = await _mediator.Send(new GetAllInsuranceClassesQuery());
            return result.ToActionResult();
        }

        [HttpGet("api/v1/InsuranceClass/GetById/{id}")]
        public async Task<IActionResult> GetClassByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetInsuranceClassByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceClass.AddInsuranceClass)]
        public async Task<IActionResult> AddClassAsync(AddInsuranceClassCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceClass.UpdateInsuranceClass)]
        public async Task<IActionResult> UpdateClassAsync(UpdateInsuranceClassCommand command)
        {
            if (command is null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceClass.DeleteInsuranceClass + "/{id}")]
        public async Task<IActionResult> DeleteClassAsync(int id)
        {
            if (id == 0) return BadRequest("Id isn't valid");

            var result = await _mediator.Send(new DeleteInsuranceClassCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
