using HarborManagementAPI.Models;
using HarborManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HarborManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarborManagementController : ControllerBase
    {
        private readonly HarborManagementService _service;
        public HarborManagementController(HarborManagementService service) { 
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Ship>> GetCurrentShips()
        {
            return await _service.GetShipList();
        }


    }
}
