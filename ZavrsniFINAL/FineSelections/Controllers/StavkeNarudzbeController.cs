using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FineSelections.Data;
using FineSelections.Models;

namespace FineSelections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StavkeNarudzbeController : ControllerBase
    {
        private readonly WebshopContext _context;

        public StavkeNarudzbeController(WebshopContext context)
        {
            _context = context;
        }

        // GET: api/StavkeNarudzbe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StavkaNarudzbe>>> GetStavkeNarudzbe()
        {
            return await _context.StavkeNarudzbe
                .Include(sn => sn.Narudzba)
                .Include(sn => sn.Proizvod)
                .ToListAsync();
        }

        // GET: api/StavkeNarudzbe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StavkaNarudzbe>> GetStavkaNarudzbe(int id)
        {
            var stavkaNarudzbe = await _context.StavkeNarudzbe
                .Include(sn => sn.Narudzba)
                .Include(sn => sn.Proizvod)
                .FirstOrDefaultAsync(sn => sn.IdStavkeNarudzbe == id);

            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }

            return stavkaNarudzbe;
        }

        // GET: api/StavkeNarudzbe/narudzba/5
        [HttpGet("narudzba/{narudzbaId}")]
        public async Task<ActionResult<IEnumerable<StavkaNarudzbe>>> GetStavkeByNarudzba(int narudzbaId)
        {
            var stavke = await _context.StavkeNarudzbe
                .Include(sn => sn.Narudzba)
                .Include(sn => sn.Proizvod)
                .Where(sn => sn.IdNarudzbe == narudzbaId)
                .ToListAsync();

            return stavke;
        }

        // PUT: api/StavkeNarudzbe/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStavkaNarudzbe(int id, StavkaNarudzbe stavkaNarudzbe)
        {
            if (id != stavkaNarudzbe.IdStavkeNarudzbe)
            {
                return BadRequest();
            }

            _context.Entry(stavkaNarudzbe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StavkaNarudzbeExists(id))
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

        // POST: api/StavkeNarudzbe
        [HttpPost]
        public async Task<ActionResult<StavkaNarudzbe>> PostStavkaNarudzbe(StavkaNarudzbe stavkaNarudzbe)
        {
            _context.StavkeNarudzbe.Add(stavkaNarudzbe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStavkaNarudzbe", new { id = stavkaNarudzbe.IdStavkeNarudzbe }, stavkaNarudzbe);
        }

        // DELETE: api/StavkeNarudzbe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStavkaNarudzbe(int id)
        {
            var stavkaNarudzbe = await _context.StavkeNarudzbe.FindAsync(id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }

            _context.StavkeNarudzbe.Remove(stavkaNarudzbe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StavkaNarudzbeExists(int id)
        {
            return _context.StavkeNarudzbe.Any(e => e.IdStavkeNarudzbe == id);
        }
    }
}
