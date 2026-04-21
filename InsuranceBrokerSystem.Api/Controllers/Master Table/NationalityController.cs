

using InsuranceBrokerSystem.Application.DTOs.Master_Table.Nationality;
using InsuranceBrokerSystem.Application.Features.Nationalities.Commands;

namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    public class NationalityController : ControllerBase
    {
        private readonly ISender _mediator;

        public NationalityController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.Nationality.AddNationality)]
        public async Task<IActionResult> AddNationalityAsync(AddNationalityDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new AddNationalityCommand { _addNationalityDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Nationality.GetAllNationalities)]
        public async Task<IActionResult> GetAllNationalitiesAsync()
        {
            var result = await _mediator.Send(new GetAllNationalitiesQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.Nationality.GetNationalityById+"/{id}")]
        public async Task<IActionResult> GetNationalityByIdAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new GetNationalityByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.Nationality.UpdateNationality)]
        public async Task<IActionResult> UpdateNationalityAsync(UpdateNationalityDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new UpdateNationalityCommand { _updateNationalityDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.Nationality.DeleteNationality+"/{id}")]
        public async Task<IActionResult> DeleteNationalityAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteNationalityCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
