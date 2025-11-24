using CarManager.Api.Data;
using CarManager.Api.DTOs.VehicleDTO;
using CarManager.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly CarManagerDbContext _db;

    public VehiclesController(CarManagerDbContext db)
    {
        _db = db;
    }

    // GET: api/vehicles
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAll()
    {
        var vehicles = await _db.Vehicles
            .OrderBy(v => v.Plate)
            .ToListAsync();

        var result = vehicles.Select(v => v.ToDto());
        return Ok(result);
    }

    // GET: api/vehicles/{id}
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleDTO>> GetById(int id)
    {
        var vehicle = await _db.Vehicles.FindAsync(id);

        if (vehicle == null)
            return NotFound();

        return Ok(vehicle.ToDto());
    }

    // POST: api/vehicles
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<VehicleDTO>> Create([FromBody] CreateVehicleDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // controllo targa unica
        var exists = await _db.Vehicles.AnyAsync(v => v.Plate == dto.Plate);
        if (exists)
        {
            ModelState.AddModelError(nameof(dto.Plate), "Targa già esistente.");
            return ValidationProblem(ModelState);
        }

        var vehicle = dto.ToEntity();

        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();

        var result = vehicle.ToDto();

        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, result);
    }

    // PUT: api/vehicles/{id}
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _db.Vehicles.FindAsync(id);
        if (vehicle == null)
            return NotFound();

        dto.UpdateEntity(vehicle);

        await _db.SaveChangesAsync();
        return NoContent(); // o Ok(vehicle.ToDto());
    }

    // DELETE: api/vehicles/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _db.Vehicles.FindAsync(id);
        if (vehicle == null)
            return NotFound();

        _db.Vehicles.Remove(vehicle);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/vehicles/search/{plate}
    [HttpGet("search/{plate}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetByPlate(string plate)
    {

        if (string.IsNullOrWhiteSpace(plate))
        {
            return BadRequest("Il parametro 'targa/plate' è obbligatorio");
        }
        var term = plate.Trim();

        var vehicles = await _db.Vehicles
            .Where(o => o.Plate.Contains(term))
            .ToListAsync();

        if (vehicles.Count == 0)
        {
            return NoContent();
        }

        var result = vehicles.Select(o => o.ToDto());
        return Ok(result);

    }

}