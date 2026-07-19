using ElearingEnglis.services.Drug;
using ElearingEnglis.services.Drug.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ElearingEnglis.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _service;
        public DrugController(IDrugService service)
        {
            _service = service;
        }

        [HttpGet("{SearchTerm}")]
        public async Task<ActionResult<List<DrugDto>>> GetDrugs(string SearchTerm)
        {
            var drugs= await _service.GetDrugsAsync(SearchTerm);
            return drugs;
        }
    }
}
