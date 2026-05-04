using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.Data.UnitOfWork;
using VehicleManager.Shared.DTOs;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OwnersController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<OwnersController> _logger;

    public OwnersController(IUnitOfWork uow, IMapper mapper, ILogger<OwnersController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    // GET api/owners
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var owners = await _uow.Owners.GetAllWithVehiclesAsync();
        return Ok(_mapper.Map<IEnumerable<OwnerDto>>(owners));
    }

    // GET api/owners/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var owner = await _uow.Owners.GetByIdWithVehiclesAsync(id);
        if (owner == null)
            return NotFound(new { message = $"Proprietario con id {id} non trovato." });

        return Ok(_mapper.Map<OwnerDto>(owner));
    }

    // GET api/owners/patente-in-scadenza?giorni=30
    [HttpGet("patente-in-scadenza")]
    [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConPatenteInScadenza([FromQuery] int giorni = 30)
    {
        var owners = await _uow.Owners.GetConPatenteInScadenzaAsync(giorni);
        return Ok(_mapper.Map<IEnumerable<OwnerDto>>(owners));
    }

    // GET api/owners/visita-in-scadenza?giorni=30
    [HttpGet("visita-in-scadenza")]
    [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConVisitaInScadenza([FromQuery] int giorni = 30)
    {
        var owners = await _uow.Owners.GetConVisitaInScadenzaAsync(giorni);
        return Ok(_mapper.Map<IEnumerable<OwnerDto>>(owners));
    }

    // POST api/owners
    [HttpPost]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var owner = _mapper.Map<Owner>(dto);
        await _uow.Owners.AddAsync(owner);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("Proprietario creato: {Nome} {Cognome}", owner.Nome, owner.Cognome);

        var result = await _uow.Owners.GetByIdWithVehiclesAsync(owner.Id);
        return CreatedAtAction(nameof(GetById), new { id = owner.Id }, _mapper.Map<OwnerDto>(result));
    }

    // PUT api/owners/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOwnerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var owner = await _uow.Owners.GetByIdAsync(id);
        if (owner == null)
            return NotFound(new { message = $"Proprietario con id {id} non trovato." });

        _mapper.Map(dto, owner);
        _uow.Owners.Update(owner);
        await _uow.SaveChangesAsync();

        var result = await _uow.Owners.GetByIdWithVehiclesAsync(id);
        return Ok(_mapper.Map<OwnerDto>(result));
    }

    // DELETE api/owners/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        var owner = await _uow.Owners.GetByIdWithVehiclesAsync(id);
        if (owner == null)
            return NotFound(new { message = $"Proprietario con id {id} non trovato." });

        if (owner.Veicoli.Any())
            return Conflict(new { message = "Impossibile eliminare: il proprietario ha veicoli associati." });

        _uow.Owners.Remove(owner);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    // POST api/owners/ownership — assegna un veicolo a un proprietario
    [HttpPost("ownership")]
    [ProducesResponseType(typeof(VehicleOwnershipDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssegnaVeicolo([FromBody] CreateVehicleOwnershipDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _uow.Vehicles.GetByIdAsync(dto.VehicleId);
        if (vehicle == null)
            return BadRequest(new { message = $"Veicolo con id {dto.VehicleId} non trovato." });

        var owner = await _uow.Owners.GetByIdAsync(dto.OwnerId);
        if (owner == null)
            return BadRequest(new { message = $"Proprietario con id {dto.OwnerId} non trovato." });

        var ownership = _mapper.Map<VehicleOwnership>(dto);

        await _uow.BeginTransactionAsync();
        try
        {
            // Recupera e chiudi l'eventuale proprietà attuale sullo stesso veicolo
            var attuali = await _uow.Vehicles.FindAsync(v => v.Id == dto.VehicleId);
            // Le ownership vengono gestite tramite il DbContext direttamente
            var existingOwnership = await _uow.Vehicles
                .FirstOrDefaultAsync(v => v.Id == dto.VehicleId);

            // Aggiungi la nuova ownership
            vehicle.Proprietari.Add(ownership);
            _uow.Vehicles.Update(vehicle);

            await _uow.SaveChangesAsync();
            await _uow.CommitTransactionAsync();
        }
        catch
        {
            await _uow.RollbackTransactionAsync();
            throw;
        }

        return CreatedAtAction(nameof(GetById), new { id = dto.OwnerId }, _mapper.Map<VehicleOwnershipDto>(ownership));
    }
}