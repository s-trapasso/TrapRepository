using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.Data.UnitOfWork;
using VehicleManager.Shared.DTOs;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MaintenancesController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<MaintenancesController> _logger;

    public MaintenancesController(IUnitOfWork uow, IMapper mapper, ILogger<MaintenancesController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    // GET api/maintenances?vehicleId=5
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaintenanceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int? vehicleId)
    {
        IEnumerable<Maintenance> list;

        if (vehicleId.HasValue)
            list = await _uow.Maintenances.GetByVehicleAsync(vehicleId.Value);
        else
            list = await _uow.Maintenances.GetAllAsync();

        return Ok(_mapper.Map<IEnumerable<MaintenanceDto>>(list));
    }

    // GET api/maintenances/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MaintenanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var m = await _uow.Maintenances.GetByIdWithDocumentsAsync(id);
        if (m == null)
            return NotFound(new { message = $"Manutenzione con id {id} non trovata." });

        return Ok(_mapper.Map<MaintenanceDto>(m));
    }

    // GET api/maintenances/vehicle/5/totale-costi
    [HttpGet("vehicle/{vehicleId:int}/totale-costi")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotaleCosti(int vehicleId)
    {
        var totale = await _uow.Maintenances.GetTotaleCostiAsync(vehicleId);
        return Ok(totale);
    }

    // POST api/maintenances
    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _uow.Vehicles.GetByIdAsync(dto.VehicleId);
        if (vehicle == null)
            return BadRequest(new { message = $"Veicolo con id {dto.VehicleId} non trovato." });

        var maintenance = _mapper.Map<Maintenance>(dto);
        await _uow.Maintenances.AddAsync(maintenance);

        // Aggiorna i km del veicolo se quelli dell'intervento sono maggiori
        if (dto.KmAlMomento.HasValue && dto.KmAlMomento.Value > vehicle.KmAttuali)
        {
            vehicle.KmAttuali = dto.KmAlMomento.Value;
            _uow.Vehicles.Update(vehicle);
        }

        await _uow.SaveChangesAsync();

        _logger.LogInformation("Manutenzione creata per veicolo {VehicleId}: {Tipo}", dto.VehicleId, dto.Tipo);

        var result = await _uow.Maintenances.GetByIdWithDocumentsAsync(maintenance.Id);
        return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, _mapper.Map<MaintenanceDto>(result));
    }

    // PUT api/maintenances/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(MaintenanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var maintenance = await _uow.Maintenances.GetByIdAsync(id);
        if (maintenance == null)
            return NotFound(new { message = $"Manutenzione con id {id} non trovata." });

        _mapper.Map(dto, maintenance);
        _uow.Maintenances.Update(maintenance);
        await _uow.SaveChangesAsync();

        var result = await _uow.Maintenances.GetByIdWithDocumentsAsync(id);
        return Ok(_mapper.Map<MaintenanceDto>(result));
    }

    // DELETE api/maintenances/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var maintenance = await _uow.Maintenances.GetByIdAsync(id);
        if (maintenance == null)
            return NotFound(new { message = $"Manutenzione con id {id} non trovata." });

        _uow.Maintenances.Remove(maintenance);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}