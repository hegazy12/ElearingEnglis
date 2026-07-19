using ElearingEnglis.services.Response;
using ElearingEnglis.services.VitalSignMaster;
using ElearingEnglis.services.VitalSignMaster.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElearingEnglis.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class VitalSignMasterController : ControllerBase
    {

        private readonly IVitalSignMasterService _service;
        public VitalSignMasterController(IVitalSignMasterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<VitalSignMasterDto>>> CreateVitalSignMaster([FromBody]CreateVitalSignMasterDto dto)
        {
            var userid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(userid);
            var resposne = await _service.CreateVitalSignMasterAsync(userid, dto);
            return StatusCode(resposne.Success ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest, resposne);
        }
    }
}
