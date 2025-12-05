using CarManager.Api.Data;
using CarManager.Api.DTOs.MaintenanceDTO;
using CarManager.Api.DTOs.MaintenanceDTO;
using CarManager.Api.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CarManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly CarManagerDbContext _db;

        public MaintenanceController(CarManagerDbContext db)
        {
            _db = db;
        }

        // GET: api/maintenance
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MaintenanceDTO>>> GetAll()
        {
            var maintenance = await _db.Maintenances
                .Include(m => m.Vehicle)
                .OrderBy(v => v.Id)
                .ToListAsync();

            var result = maintenance.Select(v => v.ToDto());
            return Ok(result);
        }

        // GET: api/maintenance/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MaintenanceDTO>> GetById(int id)
        {
            var maintenance = await _db.Maintenances
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (maintenance == null)
                return NotFound();

            return Ok(maintenance.ToDto());
        }

        // POST: api/maintenance
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MaintenanceDTO>> Create([FromBody] CreateMaintenanceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Controllo che il veicolo esista
            var vehicleExists = await _db.Vehicles
                .AnyAsync(v => v.Id == dto.VehicleId);
            if (!vehicleExists)
            {
                ModelState.AddModelError(nameof(dto.VehicleId), "Veicolo non trovato.");
                return ValidationProblem(ModelState);
            }

            var maintenance = dto.ToEntity();

            _db.Maintenances.Add(maintenance);
            await _db.SaveChangesAsync();

            var result = maintenance.ToDto();

            return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, result);
        }

        // PUT: api/maintenance/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var maintenance = await _db.Maintenances.FindAsync(id);
            if (maintenance == null)
                return NotFound();

            // controllo che il veicolo esista
            var vehicleExists = await _db.Vehicles.AnyAsync(v => v.Id == dto.VehicleId);
            if (!vehicleExists)
            {
                ModelState.AddModelError(nameof(dto.VehicleId), "Veicolo non trovato.");
                return ValidationProblem(ModelState);
            }
            dto.UpdateEntity(maintenance);

            await _db.SaveChangesAsync();
            return NoContent(); // o Ok(maintenance.ToDto());
        }

        // DELETE: api/maintenance/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var maintenance = await _db.Maintenances.FindAsync(id);
            if (maintenance == null)
                return NotFound();

            _db.Maintenances.Remove(maintenance);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/maintenances/search/{plate}
        [HttpGet("vehicle/{vehicleId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<MaintenanceDTO>>> GetByVehicle(int vehicleId)
        {
            var maintenances = await _db.Maintenances
                .Where(m => m.VehicleId == vehicleId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();

            var result = maintenances.Select(m => m.ToDto());
            return Ok(result);
        }
    }
}
