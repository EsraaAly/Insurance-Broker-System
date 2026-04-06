
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{

    [ApiController]
    public class InsuranceContactController : ControllerBase
    {
        private readonly ISender _mediator;

        public InsuranceContactController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceCompContact.GetInsuranceContactByInsuranceIdAsync + "/{id}")]
        public async Task<IActionResult> GetInsuranceContactByInsuranceIdAsync(int id)
        {
            var result = await _mediator.Send(new GetInsuranceContactsByCompanyIdQuery { CompanyId = id });
            return result.ToActionResult();
        }

    }
}
