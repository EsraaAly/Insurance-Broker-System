
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
            var classes = await _insuranceClassService.GetAllClassesAsync();
            return Ok(classes);
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceClass.AddInsuranceClass)]
        public async Task<IActionResult> AddClassAsync(AddInsuranceClassDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var success = await _insuranceClassService.AddClassAsync(dto);

            if (success)
            {
                return Ok(new { message = "Insurance Class added successfully" });
            }

            return BadRequest("Failed to add Insurance Class. Please check your data.");
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceClass.UpdateInsuranceClass)]
        public async Task<IActionResult> UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            if (dto is null) return BadRequest("Data is null");

            var entry = await _insuranceClassService.UpdateClassAsync(dto);

            if (entry != null)
            {
                return Ok(new { message = "Insurance Class updated successfully" });
            }

            return BadRequest("Failed to update Insurance Class. Please check your data.");
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceClass.DeleteInsuranceClass + "/{id}")]
        public async Task<IActionResult> DeleteClassAsync(int id)
        {
            if (id == 0) return BadRequest("Id is't Valid");

            var success = await _insuranceClassService.DeleteClassAsync(id);

            if (success)
            {
                return Ok(new { message = "Insurance Class Deleted successfully" });
            }

            return BadRequest("Failed to Delete Insurance Class. Please check your data.");
        }
    }
}
