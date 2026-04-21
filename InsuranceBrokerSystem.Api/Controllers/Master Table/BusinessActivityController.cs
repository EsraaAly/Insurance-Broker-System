using InsuranceBrokerSystem.Application.DTOs.Master_Table.BusinessActivity;
using InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands;

namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class BusinessActivityController : ControllerBase
    {
        private readonly ISender _mediator;

        public BusinessActivityController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.BusinessActivity.AddBusinessActivity)]
        public async Task<IActionResult> AddBusinessActivityAsync(AddBusinessActivityDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new AddBusinessActivityCommand { _addBusinessActivityDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.BusinessActivity.GetAllBusinessActivities)]
        public async Task<IActionResult> GetAllBusinessActivitiesAsync()
        {
            var result = await _mediator.Send(new GetAllBusinessActivitiesQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.BusinessActivity.GetBusinessActivityById+"/{id}")]
        public async Task<IActionResult> GetBusinessActivityByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetBusinessActivityByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.BusinessActivity.UpdateBusinessActivity)]
        public async Task<IActionResult> UpdateBusinessActivityAsync(UpdateBusinessActivityDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new UpdateBusinessActivityCommand { _updateBusinessActivityDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.BusinessActivity.DeleteBusinessActivity+"{id}")]
        public async Task<IActionResult> DeleteBusinessActivityAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteBusinessActivityCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
