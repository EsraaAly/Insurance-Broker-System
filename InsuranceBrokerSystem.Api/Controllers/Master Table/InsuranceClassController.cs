
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    //[Route("api/[controller]")]
    [ApiController]
    public class InsuranceClassController : ControllerBase
    {
        private readonly IInsuranceClassService _insuranceClassService;

        public InsuranceClassController(IInsuranceClassService insuranceClassService)
        {
            _insuranceClassService = insuranceClassService;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceClass.GetAllInsuranceClasses)]
        public async Task<IActionResult> GetAllClassesAsync()
        {
            var result = await _insuranceClassService.GetAllClassesAsync();
            return result.ToActionResult();
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceClass.AddInsuranceClass)]
        public async Task<IActionResult> AddClassAsync(AddInsuranceClassDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var result = await _insuranceClassService.AddClassAsync(dto);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceClass.UpdateInsuranceClass)]
        public async Task<IActionResult> UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            if (dto is null) return BadRequest("Data is null");

            var result = await _insuranceClassService.UpdateClassAsync(dto);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceClass.DeleteInsuranceClass + "/{id}")]
        public async Task<IActionResult> DeleteClassAsync(int id)
        {
            if (id == 0) return BadRequest("Id isn't valid");

            var result = await _insuranceClassService.DeleteClassAsync(id);
            return result.ToActionResult();
        }
    }
}
