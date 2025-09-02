using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FineSelections.Data;
using FineSelections.Models;

namespace FineSelections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NarudzbeController : ControllerBase
    {
        private readonly WebshopContext _context;

        public NarudzbeController(WebshopContext context)
        {
            _context = context;
        }

        // GET: api/Narudzbe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Narudzba>>> GetNarudzbe()
        {
            return await _context.Narudzbe
                .Include(n => n.Korisnik)
                .Include(n => n.StavkeNarudzbe)
                    .ThenInclude(sn => sn.Proizvod)
                .ToListAsync();
        }

        // GET: api/Narudzbe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Narudzba>> GetNarudzba(int id)
        {
            var narudzba = await _context.Narudzbe
                .Include(n => n.Korisnik)
                .Include(n => n.StavkeNarudzbe)
                    .ThenInclude(sn => sn.Proizvod)
                .FirstOrDefaultAsync(n => n.IdNarudzbe == id);

            if (narudzba == null)
            {
                return NotFound();
            }

            return narudzba;
        }

        // GET: api/Narudzbe/korisnik/5
        [HttpGet("korisnik/{korisnikId}")]
        public async Task<ActionResult<IEnumerable<Narudzba>>> GetNarudzbeByKorisnik(int korisnikId)
        {
            var narudzbe = await _context.Narudzbe
                .Include(n => n.StavkeNarudzbe)
                    .ThenInclude(sn => sn.Proizvod)
                .Where(n => n.IdKorisnika == korisnikId)
                .OrderByDescending(n => n.DatumNarudzbe)
                .ToListAsync();

            return narudzbe;
        }

        // PUT: api/Narudzbe/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNarudzba(int id, Narudzba narudzba)
        {
            if (id != narudzba.IdNarudzbe)
            {
                return BadRequest();
            }

            _context.Entry(narudzba).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarudzbaExists(id))
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

        // POST: api/Narudzbe
        [HttpPost]
        public async Task<ActionResult<Narudzba>> PostNarudzba(Narudzba narudzba)
        {
            _context.Narudzbe.Add(narudzba);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNarudzba", new { id = narudzba.IdNarudzbe }, narudzba);
        }

        // DELETE: api/Narudzbe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNarudzba(int id)
        {
            var narudzba = await _context.Narudzbe.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }

            _context.Narudzbe.Remove(narudzba);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzbe.Any(e => e.IdNarudzbe == id);
        }
    }
}
