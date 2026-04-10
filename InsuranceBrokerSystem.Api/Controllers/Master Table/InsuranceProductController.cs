
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public InsuranceProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceCompProduct.GetInsuranceProductByInsuranceId + "/{id}")]
        public async Task<IActionResult> GetInsuranceProductByInsuranceIdAsync(int id)
        {
            var result = await _mediator.Send(new GetInsuranceProductsByCompanyIdQuery { CompanyId = id });
            return result.ToActionResult();
        }

    }
}
