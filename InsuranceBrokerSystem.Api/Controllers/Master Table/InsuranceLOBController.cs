
namespace InsuranceBrokerSystem.Api.Controllers.Master_Table
{
    //[Route("api/[controller]")]
    [ApiController]
    public class InsuranceLOBController : ControllerBase
    {
        private readonly IInsuranceLOBService _insuranceLOBService;

        public InsuranceLOBController(IInsuranceLOBService insuranceLOBService)
        {
            _insuranceLOBService = insuranceLOBService;
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceLOB.GetAllInsuranceLOBs)]
        public async Task<IActionResult> GetAllLOBsAsync()
        {
            var LOBs = await _insuranceLOBService.GetAllLOBsAsync();
            return Ok(LOBs);
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceLOB.GetLOBByClassIdAsync + "/{id}")]
        public async Task<IActionResult> GetLOBByClassIdAsync(int id)
        {
            var LOB = await _insuranceLOBService.GetInsuranceLOBByClassIdAsync(id);
            return Ok(LOB);
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceLOB.AddInsuranceLOB)]
        public async Task<IActionResult> AddLOBAsync(AddInsuranceLOBDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var success = await _insuranceLOBService.AddLOBAsync(dto);

            if (success)
            {
                return Ok(new { message = "Insurance LOB added successfully" });
            }

            return BadRequest("Failed to add Insurance LOB. Please check your data.");
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceLOB.UpdateInsuranceLOB)]
        public async Task<IActionResult> UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            if (dto is null) return BadRequest("Data is null");

            var entry = await _insuranceLOBService.UpdateLOBAsync(dto);

            if (entry != null)
            {
                return Ok(new { message = "Insurance LOB updated successfully" });
            }

            return BadRequest("Failed to update Insurance LOB. Please check your data.");
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceLOB.DeleteInsuranceLOB + "/{id}")]
        public async Task<IActionResult> DeleteLOBAsync(int id)
        {
            if (id == 0) return BadRequest("Id is't Valid");

            var success = await _insuranceLOBService.DeleteLOBAsync(id);

            if (success)
            {
                return Ok(new { message = "Insurance LOB Deleted successfully" });
            }

            return BadRequest("Failed to Delete Insurance LOB. Please check your data.");
        }
    }
}
