using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FineSelections.Data;
using FineSelections.Models;

namespace FineSelections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodiController : ControllerBase
    {
        private readonly WebshopContext _context;

        public ProizvodiController(WebshopContext context)
        {
            _context = context;
        }

        // GET: api/Proizvodi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proizvod>>> GetProizvodi([FromQuery] string? vrsta = null)
        {
            var query = _context.Proizvodi.AsQueryable();

            if (!string.IsNullOrEmpty(vrsta))
            {
                query = query.Where(p => p.Vrsta == vrsta);
            }

            return await query.ToListAsync();
        }

        // GET: api/Proizvodi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proizvod>> GetProizvod(int id)
        {
            var proizvod = await _context.Proizvodi.FindAsync(id);

            if (proizvod == null)
            {
                return NotFound();
            }

            return proizvod;
        }

        // GET: api/Proizvodi/vrste
        [HttpGet("vrste")]
        public async Task<ActionResult<IEnumerable<string>>> GetVrsteProizvoda()
        {
            var vrste = await _context.Proizvodi
                .Where(p => !string.IsNullOrEmpty(p.Vrsta))
                .Select(p => p.Vrsta!)
                .Distinct()
                .ToListAsync();

            return vrste;
        }

        // PUT: api/Proizvodi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProizvod(int id, Proizvod proizvod)
        {
            if (id != proizvod.IdProizvoda)
            {
                return BadRequest();
            }

            _context.Entry(proizvod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProizvodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proizvodi
        [HttpPost]
        public async Task<ActionResult<Proizvod>> PostProizvod(Proizvod proizvod)
        {
            _context.Proizvodi.Add(proizvod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProizvod", new { id = proizvod.IdProizvoda }, proizvod);
        }

        // DELETE: api/Proizvodi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProizvod(int id)
        {
            var proizvod = await _context.Proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }

            _context.Proizvodi.Remove(proizvod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvodi.Any(e => e.IdProizvoda == id);
        }
    }
}
