using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;
using InsuranceBrokerSystem.Application.Features.Positions.Commands.AddPosition;
using InsuranceBrokerSystem.Application.Features.Positions.Commands.UpdatePosition;
using InsuranceBrokerSystem.Application.Features.Positions.Commands.DeletePosition;
using InsuranceBrokerSystem.Application.Features.Positions.Queries.GetAllPositions;
using InsuranceBrokerSystem.Application.Features.Positions.Queries.GetPositionById;

namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ISender _mediator;

        public PositionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.Position.AddPosition)]
        public async Task<IActionResult> AddPositionAsync(AddPositionDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new AddPositionCommand { _addPositionDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Position.GetAllPositions)]
        public async Task<IActionResult> GetAllPositionsAsync()
        {
            var result = await _mediator.Send(new GetAllPositionsQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Position.GetPositionById+"/{id}")]
        public async Task<IActionResult> GetPositionByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetPositionByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.Position.UpdatePosition)]
        public async Task<IActionResult> UpdatePositionAsync(UpdatePositionDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new UpdatePositionCommand { _updatePositionDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.Position.DeletePosition+"/{id}")]
        public async Task<IActionResult> DeletePositionAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeletePositionCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
