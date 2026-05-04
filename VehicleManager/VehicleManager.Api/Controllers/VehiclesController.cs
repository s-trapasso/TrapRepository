using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.Data.UnitOfWork;
using VehicleManager.Shared.DTOs;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class VehiclesController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(IUnitOfWork uow, IMapper mapper, ILogger<VehiclesController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    // GET api/vehicles
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool soloAttivi = false)
    {
        var vehicles = soloAttivi
            ? await _uow.Vehicles.GetAttiviAsync()
            : await _uow.Vehicles.GetAllWithSummaryAsync();

        return Ok(_mapper.Map<IEnumerable<VehicleSummaryDto>>(vehicles));
    }

    // GET api/vehicles/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var vehicle = await _uow.Vehicles.GetByIdWithDetailsAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con id {id} non trovato." });

        return Ok(_mapper.Map<VehicleDto>(vehicle));
    }

    // GET api/vehicles/targa/AB123CD
    [HttpGet("targa/{targa}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTarga(string targa)
    {
        var vehicle = await _uow.Vehicles.GetByTargaAsync(targa);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con targa {targa} non trovato." });

        return Ok(_mapper.Map<VehicleDto>(vehicle));
    }

    // POST api/vehicles
    [HttpPost]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateVehicleDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Controlla targa duplicata
        var esistente = await _uow.Vehicles.GetByTargaAsync(dto.Targa);
        if (esistente != null)
            return Conflict(new { message = $"Esiste già un veicolo con targa {dto.Targa}." });

        var vehicle = _mapper.Map<Vehicle>(dto);
        await _uow.Vehicles.AddAsync(vehicle);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("Veicolo creato: {Targa} {Marca} {Modello}", vehicle.Targa, vehicle.Marca, vehicle.Modello);

        var result = await _uow.Vehicles.GetByIdWithDetailsAsync(vehicle.Id);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, _mapper.Map<VehicleDto>(result));
    }

    // PUT api/vehicles/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _uow.Vehicles.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con id {id} non trovato." });

        // Controlla targa duplicata su altro veicolo
        var duplicato = await _uow.Vehicles.GetByTargaAsync(dto.Targa);
        if (duplicato != null && duplicato.Id != id)
            return Conflict(new { message = $"Esiste già un altro veicolo con targa {dto.Targa}." });

        _mapper.Map(dto, vehicle);
        _uow.Vehicles.Update(vehicle);
        await _uow.SaveChangesAsync();

        var result = await _uow.Vehicles.GetByIdWithDetailsAsync(vehicle.Id);
        return Ok(_mapper.Map<VehicleDto>(result));
    }

    // PATCH api/vehicles/5/km
    [HttpPatch("{id:int}/km")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AggiornaKm(int id, [FromBody] int nuoviKm)
    {
        var vehicle = await _uow.Vehicles.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con id {id} non trovato." });

        vehicle.KmAttuali = nuoviKm;
        _uow.Vehicles.Update(vehicle);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/vehicles/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _uow.Vehicles.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con id {id} non trovato." });

        _uow.Vehicles.Remove(vehicle);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("Veicolo eliminato: id {Id}", id);
        return NoContent();
    }

    // GET api/vehicles/5/manutenzioni
    [HttpGet("{id:int}/manutenzioni")]
    [ProducesResponseType(typeof(IEnumerable<MaintenanceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetManutenzioni(int id)
    {
        var vehicle = await _uow.Vehicles.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con id {id} non trovato." });

        var manutenzioni = await _uow.Maintenances.GetByVehicleAsync(id);
        return Ok(_mapper.Map<IEnumerable<MaintenanceDto>>(manutenzioni));
    }

    // GET api/vehicles/5/scadenze
    [HttpGet("{id:int}/scadenze")]
    [ProducesResponseType(typeof(IEnumerable<DeadlineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScadenze(int id)
    {
        var vehicle = await _uow.Vehicles.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound(new { message = $"Veicolo con id {id} non trovato." });

        var scadenze = await _uow.Deadlines.GetByVehicleAsync(id);
        return Ok(_mapper.Map<IEnumerable<DeadlineDto>>(scadenze));
    }
}