using InsuranceBrokerSystem.Application.DTOs.Master_Table.Location;
using InsuranceBrokerSystem.Application.Features.Locations.Commands;

namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ISender _mediator;

        public LocationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.Location.AddLocation)]
        public async Task<IActionResult> AddLocationAsync(AddLocationDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new AddLocationCommand { _addLocationDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Location.GetAllLocations)]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            var result = await _mediator.Send(new GetAllLocationsQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Location.GetLocationById+"/{id}")]
        public async Task<IActionResult> GetLocationByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetLocationByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.Location.UpdateLocation)]
        public async Task<IActionResult> UpdateLocationAsync(UpdateLocationDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new UpdateLocationCommand { _updateLocationDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.Location.DeleteLocation+"{id}")]
        public async Task<IActionResult> DeleteLocationAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteLocationCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
