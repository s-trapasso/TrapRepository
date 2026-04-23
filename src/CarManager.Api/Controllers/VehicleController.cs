using CarManager.Api.DTOs.VehicleDTO;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleDTO>> Create([FromBody] CreateVehicleDTO dto)
    {
        _logger.LogInformation("Creazione nuovo veicolo con targa: {Plate}", dto.Plate);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("ModelState non valido per creazione veicolo");
            return BadRequest(ModelState);
        }

        var result = await _service.CreateAsync(dto);

        if (!result.Success)
        {
            _logger.LogWarning("Errore durante creazione veicolo: {Error}", result.Error);

            if (result.Error == VehicleError.DuplicatePlate)
            {
                ModelState.AddModelError(nameof(dto.Plate), "Targa già esistente");
                return ValidationProblem(ModelState);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Errore durante la creazione");
        }

        if (result.Vehicle == null)
        {
            _logger.LogError("Vehicle nullo dopo creazione");
            return StatusCode(StatusCodes.Status500InternalServerError, "Errore interno");
        }

        _logger.LogInformation("Veicolo creato con Id: {Id}", result.Vehicle.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Vehicle.Id },
            result.Vehicle
        );
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
            return BadRequest(ModelState);
        }

        var result = await _service.UpdateAsync(id, dto);

        if (!result.Success)
        {
            _logger.LogWarning("Errore update veicolo {Id}: {Error}", id, result.Error);
            return StatusCode(StatusCodes.Status500InternalServerError, "Errore durante l'aggiornamento");
        }

        _logger.LogInformation("Veicolo {Id} aggiornato con successo", id);

        return NoContent();
    }

    // DELETE: api/vehicles/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Cancellazione veicolo {Id}", id);
        var success = await _service.DeleteAsync(id);
        if (!success)
        {
            _logger.LogWarning("Veicolo {Id} non trovato per la cancellazione", id);
            return NotFound();
        }
        _logger.LogInformation("Veicolo {Id} rimosso dal database", id);

        return NoContent();
    }



}