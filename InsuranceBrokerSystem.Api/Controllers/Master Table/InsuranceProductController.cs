namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceProductController : ControllerBase
    {
        private readonly IInsuranceProductService _InsuranceProductService;

        public InsuranceProductController(IInsuranceProductService InsuranceProductService)
        {
            _InsuranceProductService = InsuranceProductService;
        }

        
        [HttpGet(ApiRoutes.MasterTable.InsuranceCompProduct.GetInsuranceProductByInsuranceIdAsync + "/{id}")]
        public async Task<IActionResult> GetInsuranceProductByInsuranceIdAsync(int id)
        {
            var result = await _InsuranceProductService.GetInsuranceProductByInsuranceIdAsync(id);
            return result.ToActionResult();
        }

    }
}
