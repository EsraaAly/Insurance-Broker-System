namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NationalityController : ControllerBase
    {
        private readonly ISender _mediator;

        public NationalityController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddNationalityAsync(AddNationalityCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllNationalitiesAsync()
        //{
        //    var result = await _mediator.Send(new GetAllNationalitiesQuery());
        //    return result.ToActionResult();
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetNationalityByIdAsync(int id)
        //{
        //    if (id == 0) return BadRequest("Id is not valid");

        //    var result = await _mediator.Send(new GetNationalityByIdQuery { Id = id });
        //    return result.ToActionResult();
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateNationalityAsync(UpdateNationalityCommand command)
        {
            if (command == null) return BadRequest("Data is null");

            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationalityAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteNationalityCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
