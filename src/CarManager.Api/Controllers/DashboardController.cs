using CarManager.Api.DTOs;
using CarManager.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDTO>> Get()
        {
            var result = await _service.GetDashboardAsync();
            return Ok(result);
        }
    }
}
