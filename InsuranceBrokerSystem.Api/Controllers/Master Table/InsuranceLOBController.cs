
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceLOBController : ControllerBase
    {
        private readonly ISender _mediator;

        public InsuranceLOBController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceLOB.GetAllInsuranceLOBs)]
        public async Task<IActionResult> GetAllLOBsAsync()
        {
            var result = await _mediator.Send(new GetAllInsuranceLOBsQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceLOB.GetLOBByClassIdAsync + "/{id}")]
        public async Task<IActionResult> GetLOBByClassIdAsync(int id)
        {
            var result = await _mediator.Send(new GetInsuranceLOBByClassIdQuery { ClassId = id });
            return result.ToActionResult();
        }

        [HttpGet("api/v1/InsuranceLOB/GetById/{id}")]
        public async Task<IActionResult> GetLOBByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetInsuranceLOBByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceLOB.AddInsuranceLOB)]
        public async Task<IActionResult> AddLOBAsync(AddInsuranceLOBCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceLOB.UpdateInsuranceLOB)]
        public async Task<IActionResult> UpdateLOBAsync(UpdateInsuranceLOBCommand command)
        {
            if (command is null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceLOB.DeleteInsuranceLOB + "{id}")]
        public async Task<IActionResult> DeleteLOBAsync(int id)
        {
            if (id == 0) return BadRequest("Id isn't valid");

            var result = await _mediator.Send(new DeleteInsuranceLOBCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
