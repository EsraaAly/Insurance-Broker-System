
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceContactController : ControllerBase
    {
        private readonly IInsuranceContractService _InsuranceContractService;

        public InsuranceContactController(IInsuranceContractService InsuranceContractService)
        {
            _InsuranceContractService = InsuranceContractService;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceCompContact.GetInsuranceContactByInsuranceIdAsync + "/{id}")]
        public async Task<IActionResult> GetInsuranceContactByInsuranceIdAsync(int id)
        {

            var company = await _InsuranceContractService.GetInsuranceContactByInsuranceIdAsync(id);

            return Ok(company);
        }

    }
}
