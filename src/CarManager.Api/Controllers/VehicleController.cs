using CarManager.Api.Common;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{

    private readonly IVehicleService _service;
    private readonly ILogger<VehiclesController> _logger;
    public VehiclesController(IVehicleService service, ILogger<VehiclesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/vehicles
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAll([FromQuery] string? plate)
    {
        _logger.LogInformation("Recupero veicoli filtro plate: {Plate}", plate);

        if (!string.IsNullOrWhiteSpace(plate))
        {
            var filtered = await _service.SearchByPlateAsync(plate);
            return Ok(filtered);
        }

        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/vehicles/{id}
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VehicleDTO>> GetById(int id)
    {
        _logger.LogInformation("Ricerca veicolo con Id: {Id}", id);

        var vehicle = await _service.GetByIdAsync(id);

        if (vehicle == null)
        {
            _logger.LogWarning("Veicolo {Id} non trovato", id);
            return NotFound();
        }

        return Ok(vehicle);
    }

    // POST: api/vehicles
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleDTO dto)
    {
        _logger.LogInformation("Creazione nuovo veicolo con targa: {Plate}", dto.Plate);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ModelState non valido per creazione veicolo");
            return ValidationProblem(ModelState);
        }

        var result = await _service.CreateAsync(dto);
        
        if (result.Success)
        {
            _logger.LogInformation("Vehicle created successfully with Id: {Id}", result.Data.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

           
        
        return result.ToActionResult();
    }

    // PUT: api/vehicles/{id}
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleDTO dto)
    {
        _logger.LogInformation("Aggiornamento veicolo Id: {Id}", id);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ModelState non valido per update veicolo {Id}", id);
            return ValidationProblem(ModelState);
        }

        var result = await _service.UpdateAsync(id, dto);
        if (!result.Success)
            _logger.LogWarning("Update failed for Vehicle {Id}", id);

        return result.ToActionResult();
    }

    // DELETE: api/vehicles/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Cancellazione veicolo {Id}", id);
        var result = await _service.DeleteAsync(id);
        if (!result.Success)
            _logger.LogWarning("Delete failed for Vehicle {Id}", id);
        return result.ToActionResult();
    }



}