using CarManager.Api.Data;
using CarManager.Api.DTOs.Owner;
using CarManager.Api.Mappings;
using CarManager.Core.Models;
using CodiceFiscaleLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly CarManagerDbContext _db;
        private readonly ILogger<OwnersController> _logger;
        public OwnersController(CarManagerDbContext db, ILogger<OwnersController> logger)
        {
            _db = db;
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
                var owners = await _db.Owners
                .OrderBy(v => v.Id)
                .ToListAsync();

                var result = owners.Select(v => v.ToDto());
                _logger.LogInformation("Recuperati {Count} proprietari", owners.Count);
                return Ok(result);
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
                var owner = await _db.Owners.FindAsync(id);

                if (owner == null)
                {
                    _logger.LogWarning("Proprietario con Id {Id} non trovato", id);
                    return NotFound();
                }
                _logger.LogInformation("Proprietario con Id {Id} recuperato con successo", id);
                return Ok(owner.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del proprietario con Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante il recupero del proprietario.");
            }

        }

        // POST: api/owners
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OwnerDTO>> Create([FromBody] CreateOwnerDTO dto)
        {
            // 1) Prima cosa: validazione DataAnnotations del DTO
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dati di creazione proprietario non validi {@Dto}", dto);
                return BadRequest(ModelState);
            }
            
            // 2) Controllo che i dati per il calcolo del CF siano sufficienti
            //    (qui usi i nomi che hai nel DTO: FirstName, LastName, BirthDate, BirthPlace, Gender, ecc.)
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                dto.BirthDate == default ||
                string.IsNullOrWhiteSpace(dto.BirthPlace) ||
                string.IsNullOrWhiteSpace(dto.Gender.ToString()))
            {
                _logger.LogWarning("Dati insufficienti per il calcolo del codice fiscale {@Dto}", dto);
                ModelState.AddModelError(nameof(Owner.FiscalCode), "Dati insufficienti per calcolare il codice fiscale.");
                return ValidationProblem(ModelState);
            }

            try
            {
                _logger.LogInformation("Calcolo del codice fiscale per il nuovo proprietario {@Dto}", dto);
                // 3) Calcolo del codice fiscale tramite libreria
                //    (qui assumo che Gender sia "M"/"F" stringa; se è già char, togli il char.Parse)

                var fiscalCode = CodiceFiscaleLib.Helpers.EncodingHelper.Encode(
                    dto.LastName,
                    dto.FirstName,
                    dto.Gender == Core.Enums.OwnerGender.Male ? 'M' : 'F',
                    dto.BirthDate,
                    dto.BirthPlace            // qui dovrebbe essere il CODICE del comune, non il nome
                );

                // 4) Controllo unicità codice fiscale
                var exists = await _db.Owners.AnyAsync(v => v.FiscalCode == fiscalCode);
                if (exists)
                {
                    _logger.LogWarning("Codice fiscale {FiscalCode} già esistente", fiscalCode);

                    ModelState.AddModelError(nameof(Owner.FiscalCode), "Codice fiscale già esistente.");
                    return ValidationProblem(ModelState);
                }

                // 5) Mappatura DTO -> entità e assegnazione CF
                var owner = dto.ToEntity();
                owner.FiscalCode = fiscalCode;

                _db.Owners.Add(owner);
                await _db.SaveChangesAsync();

                var result = owner.ToDto();
                _logger.LogInformation("Proprietario creato con Id {Id}", owner.Id);
                return CreatedAtAction(nameof(GetById), new { id = owner.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il calcolo del codice fiscale per {@Dto}", dto);
                ModelState.AddModelError(nameof(Owner.FiscalCode), "Codice fiscale non valido.");
                return ValidationProblem(ModelState);
            }
        }


        // PUT: api/owner/{id}
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
            var owner = await _db.Owners.FindAsync(id);
            if (owner == null)
            {
                _logger.LogWarning("Proprietario con Id {Id} non trovato per l'aggiornamento", id);
                return NotFound();
            }
                

            dto.UpdateEntity(owner);

            await _db.SaveChangesAsync();
            _logger.LogInformation("Proprietario con Id {Id} aggiornato con successo", id);
            return NoContent(); // o Ok(owner.ToDto());
        }

        // DELETE: api/owner/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Cancellazione del proprietario con Id {Id}", id);
            try
            {
                var owner = await _db.Owners.FindAsync(id);
                if (owner == null)
                {
                    _logger.LogWarning("Proprietario con Id {Id} non trovato per la cancellazione", id);
                    return NotFound();
                }
                   
                _db.Owners.Remove(owner);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Proprietario con Id {Id} cancellato con successo", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la cancellazione del proprietario con Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore del server durante la cancellazione del proprietario.");
            }
        }

        //GET: api/owners/search/{name}
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

            var owners = await _db.Owners
                .Where(o => o.FirstName.Contains(term) || o.LastName.Contains(term))
                .ToListAsync();

            if (owners.Count == 0)
            {
                _logger.LogInformation("Nessun proprietario trovato per il termine di ricerca '{Name}'", name);
                return NoContent();
            }

            var result = owners.Select(o => o.ToDto());
            _logger.LogInformation("{Count} proprietari trovati per il termine di ricerca '{Name}'", owners.Count, name);
            return Ok(result);

        }
        // POST: api/owners/fiscalcode/preview
        [HttpPost("fiscalcode/preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> GetFiscalCodePreview([FromBody] CreateOwnerDTO dto)
        {
            // volendo puoi usare lo stesso controllo che hai in Create
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                dto.BirthDate == default ||
                string.IsNullOrWhiteSpace(dto.BirthPlace) ||
                string.IsNullOrWhiteSpace(dto.Gender.ToString()))
            {
                _logger.LogWarning("Dati insufficienti per il calcolo del codice fiscale (preview) {@Dto}", dto);
                return BadRequest("Dati insufficienti per calcolare il codice fiscale.");
            }

            try
            {
                var fiscalCode = CodiceFiscaleLib.Helpers.EncodingHelper.Encode(
                    dto.LastName,
                    dto.FirstName,
                    char.Parse(dto.Gender.ToString()),
                    dto.BirthDate,
                    dto.BirthPlace
                );
                _logger.LogInformation("Calcolo del codice fiscale (preview) riuscito: {FiscalCode}", fiscalCode);
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
