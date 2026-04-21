
using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands;

namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceCompanyController : ControllerBase
    {
        private readonly ISender _mediator;

        public InsuranceCompanyController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceComp.AddInsuranceComp)]
        public async Task<IActionResult> AddInsuranceCompanyAsync(AddInsuranceCompanyDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new AddInsuranceCompanyCommand { _addInsuranceCompanyDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceComp.GetAllInsuranceCompanies)]
        public async Task<IActionResult> GetAllInsuranceCompaniesAsync()
        {
            var result = await _mediator.Send(new GetAllInsuranceCompaniesQuery());
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceComp.GetInsuranceCompanyByName)]
        public async Task<IActionResult> GetInsuranceCompanyByNameAsync([FromQuery] string Name)
        {
            var result = await _mediator.Send(new GetInsuranceCompanyByNameQuery { Name = Name });
            return result.ToActionResult();
        }

        [HttpGet("api/v1/InsuranceComp/GetById/{id}")]
        public async Task<IActionResult> GetInsuranceCompanyByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetInsuranceCompanyByIdQuery { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceComp.UpdateInsuranceComp)]
        public async Task<IActionResult> UpdateInsuranceCompanyAsync(UpdateInsuranceCompanyDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");
            var command = new UpdateInsuranceCompanyCommand { _updateInsuranceCompanyDTO = dto };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.Financial.InsuranceComp.ApproveInsuranceComp)]
        public async Task<IActionResult> ApproveInsuranceCompanyAsync([FromBody] int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new ApproveInsuranceCompanyCommand { Id = id });
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.Financial.InsuranceComp.RejectInsuranceComp)]
        public async Task<IActionResult> RejectInsuranceCompanyAsync([FromBody] int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new RejectInsuranceCompanyCommand { Id = id });
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceComp.DeleteInsuranceComp + "{id}")]
        public async Task<IActionResult> DeleteInsuranceCompanyAsync(int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _mediator.Send(new DeleteInsuranceCompanyCommand { Id = id });
            return result.ToActionResult();
        }
    }
}
