using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FineSelections.Data;
using FineSelections.Models;

namespace FineSelections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KosariceController : ControllerBase
    {
        private readonly WebshopContext _context;

        public KosariceController(WebshopContext context)
        {
            _context = context;
        }

        // GET: api/Kosarice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kosarica>>> GetKosarice()
        {
            return await _context.Kosarice
                .Include(k => k.Korisnik)
                .Include(k => k.StavkeKosarice)
                    .ThenInclude(sk => sk.Proizvod)
                .ToListAsync();
        }

        // GET: api/Kosarice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kosarica>> GetKosarica(int id)
        {
            var kosarica = await _context.Kosarice
                .Include(k => k.Korisnik)
                .Include(k => k.StavkeKosarice)
                    .ThenInclude(sk => sk.Proizvod)
                .FirstOrDefaultAsync(k => k.IdKosarice == id);

            if (kosarica == null)
            {
                return NotFound();
            }

            return kosarica;
        }

        // GET: api/Kosarice/korisnik/5
        [HttpGet("korisnik/{korisnikId}")]
        public async Task<ActionResult<IEnumerable<Kosarica>>> GetKosariceByKorisnik(int korisnikId)
        {
            var kosarice = await _context.Kosarice
                .Include(k => k.StavkeKosarice)
                    .ThenInclude(sk => sk.Proizvod)
                .Where(k => k.IdKorisnika == korisnikId)
                .ToListAsync();

            return kosarice;
        }

        // PUT: api/Kosarice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKosarica(int id, Kosarica kosarica)
        {
            if (id != kosarica.IdKosarice)
            {
                return BadRequest();
            }

            _context.Entry(kosarica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KosaricaExists(id))
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

        // POST: api/Kosarice
        [HttpPost]
        public async Task<ActionResult<Kosarica>> PostKosarica(Kosarica kosarica)
        {
            _context.Kosarice.Add(kosarica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKosarica", new { id = kosarica.IdKosarice }, kosarica);
        }

        // DELETE: api/Kosarice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKosarica(int id)
        {
            var kosarica = await _context.Kosarice.FindAsync(id);
            if (kosarica == null)
            {
                return NotFound();
            }

            _context.Kosarice.Remove(kosarica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KosaricaExists(int id)
        {
            return _context.Kosarice.Any(e => e.IdKosarice == id);
        }
    }
}
