using CarManager.Api.Data;
using CarManager.Api.DTOs.MaintenanceDTO;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CarManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenancesController : ControllerBase
    {
        private readonly IMaintenanceService _service;
        private readonly ILogger<MaintenancesController> _logger;

        public MaintenancesController(IMaintenanceService service, ILogger<MaintenancesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaintenanceDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("vehicle/{vehicleId:int}")]
        public async Task<ActionResult<List<MaintenanceDTO>>> GetByVehicle(int vehicleId)
        {
            var result = await _service.GetByVehicleIdAsync(vehicleId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaintenanceDTO>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateMaintenanceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto);

            if (!result.Success)
            {
                if (result.Error == "VehicleNotFound")
                    return NotFound("Veicolo non trovato");

                return StatusCode(500, "Errore creazione manutenzione");
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, dto);

            if (!result.Success)
            {
                if (result.Error == "NotFound")
                    return NotFound();

                return StatusCode(500, "Errore aggiornamento manutenzione");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
