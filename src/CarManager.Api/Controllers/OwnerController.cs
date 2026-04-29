using CarManager.Api.Data;
using CarManager.Api.DTOs.Owner;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enums;
using CarManager.Core.Models;
using CodiceFiscaleLib;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _service;
        private readonly ILogger<OwnersController> _logger;
        public OwnersController(IOwnerService service, ILogger<OwnersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/owners
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OwnerDTO>>> GetAll()
        {
            _logger.LogInformation("Recupero tutti i proprietari");
            try
            {
                var owners = await _service.GetAllAsync();
                _logger.LogInformation("Recuperati {Count} proprietari", owners.Count());
                return Ok(owners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei proprietari");
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante il recupero dei proprietari.");
            }
        }

        // GET: api/owners/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OwnerDTO>> GetById(int id)
        {
            _logger.LogInformation("Recupero il proprietario con Id {Id}", id);
            try
            {
                var owner = await _service.GetByIdAsync(id);

                if (owner == null)
                {
                    _logger.LogWarning("Proprietario con Id {Id} non trovato", id);
                    return NotFound();
                }
                _logger.LogInformation("Proprietario con Id {Id} recuperato con successo", id);
                return Ok(owner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del proprietario con Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante il recupero del proprietario.");
            }
        }

        // POST: api/owners
        // Metodo Create aggiornato per delegare completamente al service
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OwnerDTO>> Create([FromBody] CreateOwnerDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dati di creazione proprietario non validi {@Dto}", dto);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creazione nuovo proprietario {@Dto}", dto);

                var result = await _service.CreateAsync(dto);

                if (!result.Success)
                {
                    if (result.Error == ErrorCode.DuplicateFiscalCode)
                    {
                        _logger.LogWarning("Codice fiscale duplicato per {@Dto}", dto);
                        ModelState.AddModelError(nameof(dto.FiscalCode), "Codice fiscale già esistente.");
                        return ValidationProblem(ModelState);
                    }

                    if (result.Error == ErrorCode.ValidationError)
                    {
                        _logger.LogWarning("Dati non validi per il calcolo del codice fiscale {@Dto}", dto);
                        ModelState.AddModelError(nameof(dto.FiscalCode), "Dati insufficienti o codice fiscale non calcolabile.");
                        return ValidationProblem(ModelState);
                    }

                    _logger.LogError("Errore durante la creazione del proprietario: {Error}", result.Error);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante la creazione del proprietario.");
                }

                var created = result.Data!;
                _logger.LogInformation("Proprietario creato con Id {Id}", created.Id);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore imprevisto durante la creazione del proprietario {@Dto}", dto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante la creazione del proprietario.");
            }
        }

        // PUT: api/owners/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOwnerDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dati di aggiornamento proprietario non validi {@Dto}", dto);
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Aggiornamento del proprietario con Id {Id}", id);

            var result = await _service.UpdateAsync(id, dto);

            if (!result.Success)
            {
                if (result.Error == ErrorCode.OwnerNotFound)
                {
                    _logger.LogWarning("Proprietario con Id {Id} non trovato per l'aggiornamento", id);
                    return NotFound();
                }

                _logger.LogError("Errore durante l'aggiornamento del proprietario: {Error}", result.Error);
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante l'aggiornamento del proprietario.");
            }

            _logger.LogInformation("Proprietario con Id {Id} aggiornato con successo", id);
            return NoContent();
        }

        // DELETE: api/owners/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Cancellazione del proprietario con Id {Id}", id);
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result.Success)
                {
                    if (result.Error == ErrorCode.OwnerNotFound)
                    {
                        _logger.LogWarning("Proprietario con Id {Id} non trovato per la cancellazione", id);
                        return NotFound();
                    }

                    // relazione non vuota o validazione business
                    if (result.Error == ErrorCode.ValidationError)
                    {
                        _logger.LogWarning("Impossibile cancellare il proprietario {Id} per vincoli di relazione", id);
                        return BadRequest("Il proprietario non può essere cancellato: esistono relazioni attive.");
                    }

                    _logger.LogError("Errore durante la cancellazione del proprietario: {Error}", result.Error);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante la cancellazione del proprietario.");
                }

                _logger.LogInformation("Proprietario con Id {Id} cancellato con successo", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la cancellazione del proprietario con Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante la cancellazione del proprietario.");
            }
        }

        // GET: api/owners/search/{name}
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetByName(string name)
        {
            _logger.LogInformation("Ricerca dei proprietari con nome o cognome contenente '{Name}'", name);
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Parametro 'name' mancante o vuoto nella ricerca proprietari");
                return BadRequest("Il parametro 'name' è obbligatorio");
            }
            var term = name.Trim();

            var owners = await _service.SearchAsync(term);

            if (owners == null || owners.Count == 0)
            {
                _logger.LogInformation("Nessun proprietario trovato per il termine di ricerca '{Name}'", name);
                return NoContent();
            }

            _logger.LogInformation("{Count} proprietari trovati per il termine di ricerca '{Name}'", owners.Count, name);
            return Ok(owners);
        }

        // POST: api/owners/fiscalcode/preview
        // POST: api/owners/fiscalcode/preview
        [HttpPost("fiscalcode/preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> GetFiscalCodePreview([FromBody] FiscalCodePreviewDTO dto)
        {
            if (dto.Gender == OwnerGender.Unknown)
                return BadRequest("Sesso non valido.");

            try
            {
                var normalizedBirthPlace = Core.Extensions.BirthPlaceNormalizer.NormalizeBirthPlace(dto.BirthPlace);

                var fiscalCode = CodiceFiscaleLib.Helpers.EncodingHelper.Encode(
                    dto.LastName,
                    dto.FirstName,
                    dto.Gender == OwnerGender.Male ? 'M' : 'F',
                    dto.BirthDate,
                    normalizedBirthPlace
                );

                return Ok(fiscalCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel calcolo del codice fiscale (preview)");
                return BadRequest("Errore nel calcolo del codice fiscale.");
            }
        }
    }
}
