using CarManager.Api.Data;
using CarManager.Api.DTOs.OwnerDTO;
using CarManager.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly CarManagerDbContext _db;
        public OwnersController(CarManagerDbContext db)
        {
            _db = db;
        }

        // GET: api/owners
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OwnerDTO>>> GetAll()
        {
            var owners = await _db.Owners
                .OrderBy(v => v.Id)
                .ToListAsync();

            var result = owners.Select(v => v.ToDto());
            return Ok(result);
        }

        // GET: api/owners/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OwnerDTO>> GetById(int id)
        {
            var owner = await _db.Owners.FindAsync(id);

            if (owner == null)
                return NotFound();

            return Ok(owner.ToDto());
        }

        // POST: api/owners
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OwnerDTO>> Create([FromBody] CreateOwnerDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _db.Owners.AnyAsync(v => v.FiscalCode == dto.FiscalCode);
            if (exists)
            {
                ModelState.AddModelError(nameof(dto.FiscalCode), "Codice fiscale già esistente.");
                return ValidationProblem(ModelState);
            }

            var owner = dto.ToEntity();

            _db.Owners.Add(owner);
            await _db.SaveChangesAsync();

            var result = owner.ToDto();

            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, result);
        }

        // PUT: api/owner/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOwnerDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = await _db.Owners.FindAsync(id);
            if (owner == null)
                return NotFound();

            dto.UpdateEntity(owner);

            await _db.SaveChangesAsync();
            return NoContent(); // o Ok(owner.ToDto());
        }

        // DELETE: api/owner/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _db.Owners.FindAsync(id);
            if (owner == null)
                return NotFound();

            _db.Owners.Remove(owner);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        //GET: api/owners/search/{name}
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetByName(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Il parametro 'name' è obbligatorio");
            }
            var term = name.Trim();

            var owners = await _db.Owners
                .Where(o => o.FirstName.Contains(term) || o.LastName.Contains(term))
                .ToListAsync();

            if (owners.Count == 0)
            {
                return NoContent();
            }

            var result = owners.Select(o => o.ToDto());
            return Ok(result);

        }
    }
}
