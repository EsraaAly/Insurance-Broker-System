
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceCompanyController : ControllerBase
    {
        private readonly IInsuranceCompanyService _InsuranceCompanyService;

        public InsuranceCompanyController(IInsuranceCompanyService InsuranceCompanyService)
        {
            _InsuranceCompanyService = InsuranceCompanyService;
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceComp.AddInsuranceComp)]
        public async Task<IActionResult> AddInsuranceCompanyAsync(AddInsuranceCompanyDTO dto)
        {
            if (dto == null) 
            {
                return BadRequest();
            }

            await _InsuranceCompanyService.AddInsuranceCompaniesAsync(dto);

            return Ok(dto);
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceComp.GetAllInsuranceCompanies)]
        public async Task<IActionResult> GetAllInsuranceCompaniesAsync()
        {

            var company = await _InsuranceCompanyService.GetAllInsuranceCompaniesAsync();

            return Ok(company);
        }
        [HttpGet(ApiRoutes.MasterTable.InsuranceComp.GetInsuranceCompanyByName)]
        public async Task<IActionResult> GetInsuranceCompanyByNameAsync([FromQuery]string Name)
        {

            var company = await _InsuranceCompanyService.GetInsuranceCompaniesByNameAsync(Name);

            return Ok(company);
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceComp.UpdateInsuranceComp)]
        public async Task<IActionResult> UpdateInsuranceCompanyAsync(UpdateInsuranceCompanyDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            await _InsuranceCompanyService.UpdateInsuranceCompaniesAsync(dto);

            return Ok(dto);
        }

        [HttpPut(ApiRoutes.Financial.InsuranceComp.ApproveInsuranceComp)]
        public async Task<IActionResult> ApproveInsuranceCompanyAsync([FromBody] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            await _InsuranceCompanyService.ApproveInsuranceCompaniesAsync(id);

            return Ok();
        }

        [HttpPut(ApiRoutes.Financial.InsuranceComp.RejectInsuranceComp)]
        public async Task<IActionResult> RejectInsuranceCompanyAsync([FromBody] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            await _InsuranceCompanyService.RejectInsuranceCompaniesAsync(id);

            return Ok();
        }
    }
}
