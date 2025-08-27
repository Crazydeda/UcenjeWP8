using FineSelections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Controllers
{
    public class NarudzbeController : Controller
    {
        private readonly WebshopContext _ctx;
        public NarudzbeController(WebshopContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("KorisnikId");
            if (userId == null) return RedirectToAction("Create", "Korisnici");

            var list = await _ctx.Narudzbe
                .Where(n => n.ID_korisnika == userId)
                .OrderByDescending(n => n.DatumNarudzbe)
                .AsNoTracking().ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var n = await _ctx.Narudzbe
                .Include(n => n.Stavke)!.ThenInclude(s => s.Proizvod)
                .FirstOrDefaultAsync(n => n.ID_narudzbe == id);
            if (n == null) return NotFound();
            return View(n);
        }
    }
}
