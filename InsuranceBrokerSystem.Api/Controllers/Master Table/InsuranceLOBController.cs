
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
            var result = await _insuranceLOBService.GetAllLOBsAsync();
            return result.ToActionResult();
        }

        [HttpGet(ApiRoutes.MasterTable.InsuranceLOB.GetLOBByClassIdAsync + "/{id}")]
        public async Task<IActionResult> GetLOBByClassIdAsync(int id)
        {
            var result = await _insuranceLOBService.GetInsuranceLOBByClassIdAsync(id);
            return result.ToActionResult();
        }

        [HttpPost(ApiRoutes.MasterTable.InsuranceLOB.AddInsuranceLOB)]
        public async Task<IActionResult> AddLOBAsync(AddInsuranceLOBDTO dto)
        {
            if (dto == null) return BadRequest("Data is null");

            var result = await _insuranceLOBService.AddLOBAsync(dto);
            return result.ToActionResult();
        }

        [HttpPut(ApiRoutes.MasterTable.InsuranceLOB.UpdateInsuranceLOB)]
        public async Task<IActionResult> UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            if (dto is null) return BadRequest("Data is null");

            var result = await _insuranceLOBService.UpdateLOBAsync(dto);
            return result.ToActionResult();
        }

        [HttpDelete(ApiRoutes.MasterTable.InsuranceLOB.DeleteInsuranceLOB + "/{id}")]
        public async Task<IActionResult> DeleteLOBAsync(int id)
        {
            if (id == 0) return BadRequest("Id isn't valid");

            var result = await _insuranceLOBService.DeleteLOBAsync(id);
            return result.ToActionResult();
        }
    }
}
