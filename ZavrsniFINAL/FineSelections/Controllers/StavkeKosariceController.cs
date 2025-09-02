using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FineSelections.Data;
using FineSelections.Models;

namespace FineSelections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StavkeKosariceController : ControllerBase
    {
        private readonly WebshopContext _context;

        public StavkeKosariceController(WebshopContext context)
        {
            _context = context;
        }

        // GET: api/StavkeKosarice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StavkaKosarice>> GetStavkaKosarice(int id)
        {
            var stavkaKosarice = await _context.StavkeKosarice
                // .Include(sk => sk.Kosarica)
                .Include(sk => sk.Proizvod)
                .FirstOrDefaultAsync(sk => sk.IdStavke == id);

            if (stavkaKosarice == null)
            {
                return NotFound();
            }

            return stavkaKosarice;
        }

        // GET: api/StavkeKosarice/kosarica/5
        [HttpGet("kosarica/{kosaricaId}")]
        public async Task<ActionResult<IEnumerable<StavkaKosarice>>> GetStavkeByKosarica(int kosaricaId)
        {
            var stavke = await _context.StavkeKosarice
                .Include(sk => sk.Proizvod)
                .Where(sk => sk.IdKosarice == kosaricaId)
                .ToListAsync();

            return stavke;
        }

        // PUT: api/StavkeKosarice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStavkaKosarice(int id, StavkaKosarice stavkaKosarice)
        {
            if (id != stavkaKosarice.IdStavke)
            {
                return BadRequest();
            }

            _context.Entry(stavkaKosarice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StavkaKosariceExists(id))
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

        // POST: api/StavkeKosarice
        [HttpPost]
        public async Task<ActionResult<StavkaKosarice>> PostStavkaKosarice(StavkaKosarice stavkaKosarice)
        {
            _context.StavkeKosarice.Add(stavkaKosarice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStavkaKosarice", new { id = stavkaKosarice.IdStavke }, stavkaKosarice);
        }

        // DELETE: api/StavkeKosarice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStavkaKosarice(int id)
        {
            var stavkaKosarice = await _context.StavkeKosarice.FindAsync(id);
            if (stavkaKosarice == null)
            {
                return NotFound();
            }

            _context.StavkeKosarice.Remove(stavkaKosarice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StavkaKosariceExists(int id)
        {
            return _context.StavkeKosarice.Any(e => e.IdStavke == id);
        }
    }
}
