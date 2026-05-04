using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.Data.UnitOfWork;
using VehicleManager.Shared.DTOs;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DeadlinesController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<DeadlinesController> _logger;

    public DeadlinesController(IUnitOfWork uow, IMapper mapper, ILogger<DeadlinesController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    // GET api/deadlines
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeadlineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int? vehicleId)
    {
        IEnumerable<Deadline> list;

        if (vehicleId.HasValue)
            list = await _uow.Deadlines.GetByVehicleAsync(vehicleId.Value);
        else
            list = await _uow.Deadlines.GetAllWithVehicleAsync();

        return Ok(_mapper.Map<IEnumerable<DeadlineDto>>(list));
    }

    // GET api/deadlines/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DeadlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var deadline = await _uow.Deadlines.GetByIdAsync(id);
        if (deadline == null)
            return NotFound(new { message = $"Scadenza con id {id} non trovata." });

        return Ok(_mapper.Map<DeadlineDto>(deadline));
    }

    // GET api/deadlines/in-scadenza?giorni=30
    [HttpGet("in-scadenza")]
    [ProducesResponseType(typeof(IEnumerable<DeadlineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInScadenza([FromQuery] int giorni = 30)
    {
        var deadlines = await _uow.Deadlines.GetInScadenzaAsync(giorni);
        return Ok(_mapper.Map<IEnumerable<DeadlineDto>>(deadlines));
    }

    // GET api/deadlines/scadute
    [HttpGet("scadute")]
    [ProducesResponseType(typeof(IEnumerable<DeadlineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScadute()
    {
        var deadlines = await _uow.Deadlines.GetScaduteAsync();
        return Ok(_mapper.Map<IEnumerable<DeadlineDto>>(deadlines));
    }

    // GET api/deadlines/dashboard-alert
    [HttpGet("dashboard-alert")]
    [ProducesResponseType(typeof(DashboardAlertDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboardAlert()
    {
        var scadute = await _uow.Deadlines.GetScaduteAsync();
        var inScadenza = await _uow.Deadlines.GetInScadenzaAsync(30);
        var patenteAlert = await _uow.Owners.GetConPatenteInScadenzaAsync(30);
        var visitaAlert = await _uow.Owners.GetConVisitaInScadenzaAsync(30);

        var alert = new DashboardAlertDto
        {
            ScaduteCount = scadute.Count(),
            InScadenzaCount = inScadenza.Count(),
            PatenteAlertCount = patenteAlert.Count(),
            VisitaAlertCount = visitaAlert.Count(),
            Scadute = _mapper.Map<IEnumerable<DeadlineDto>>(scadute),
            InScadenza = _mapper.Map<IEnumerable<DeadlineDto>>(inScadenza),
            PatenteInScadenza = _mapper.Map<IEnumerable<OwnerDto>>(patenteAlert),
            VisitaInScadenza = _mapper.Map<IEnumerable<OwnerDto>>(visitaAlert),
        };

        return Ok(alert);
    }

    // POST api/deadlines
    [HttpPost]
    [ProducesResponseType(typeof(DeadlineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDeadlineDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = await _uow.Vehicles.GetByIdAsync(dto.VehicleId);
        if (vehicle == null)
            return BadRequest(new { message = $"Veicolo con id {dto.VehicleId} non trovato." });

        var deadline = _mapper.Map<Deadline>(dto);
        await _uow.Deadlines.AddAsync(deadline);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("Scadenza creata: {Tipo} per veicolo {VehicleId}", dto.Tipo, dto.VehicleId);

        var result = await _uow.Deadlines.GetByIdAsync(deadline.Id);
        return CreatedAtAction(nameof(GetById), new { id = deadline.Id }, _mapper.Map<DeadlineDto>(result));
    }

    // PUT api/deadlines/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DeadlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeadlineDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var deadline = await _uow.Deadlines.GetByIdAsync(id);
        if (deadline == null)
            return NotFound(new { message = $"Scadenza con id {id} non trovata." });

        _mapper.Map(dto, deadline);
        _uow.Deadlines.Update(deadline);
        await _uow.SaveChangesAsync();

        return Ok(_mapper.Map<DeadlineDto>(deadline));
    }

    // DELETE api/deadlines/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deadline = await _uow.Deadlines.GetByIdAsync(id);
        if (deadline == null)
            return NotFound(new { message = $"Scadenza con id {id} non trovata." });

        _uow.Deadlines.Remove(deadline);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}