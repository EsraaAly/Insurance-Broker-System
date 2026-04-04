
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
            if (dto == null) return BadRequest("Data is null");

            var result = await _InsuranceCompanyService.AddInsuranceCompaniesAsync(dto);
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceComp.GetAllInsuranceCompanies)]
        public async Task<IActionResult> GetAllInsuranceCompaniesAsync()
        {
            var result = await _InsuranceCompanyService.GetAllInsuranceCompaniesAsync();
            return result.ToActionResult();
        }
        [HttpGet(ApiRoutes.MasterTable.InsuranceComp.GetInsuranceCompanyByName)]
        public async Task<IActionResult> GetInsuranceCompanyByNameAsync([FromQuery] string Name)
        {
            var result = await _InsuranceCompanyService.GetInsuranceCompaniesByNameAsync(Name);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceComp.UpdateInsuranceComp)]
        public async Task<IActionResult> UpdateInsuranceCompanyAsync(UpdateInsuranceCompanyDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var result = await _InsuranceCompanyService.UpdateInsuranceCompaniesAsync(dto);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.Financial.InsuranceComp.ApproveInsuranceComp)]
        public async Task<IActionResult> ApproveInsuranceCompanyAsync([FromBody] int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _InsuranceCompanyService.ApproveInsuranceCompaniesAsync(id);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.Financial.InsuranceComp.RejectInsuranceComp)]
        public async Task<IActionResult> RejectInsuranceCompanyAsync([FromBody] int id)
        {
            if (id == 0) return BadRequest("Id is not valid");

            var result = await _InsuranceCompanyService.RejectInsuranceCompaniesAsync(id);
            return result.ToActionResult();
        }
    }
}
