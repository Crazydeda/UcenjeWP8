using FineSelections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebshopContext _ctx;
        public HomeController(WebshopContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Index(string? vrsta)
        {
            var proizvodi = _ctx.Proizvodi.AsQueryable();
            if (!string.IsNullOrWhiteSpace(vrsta))
                proizvodi = proizvodi.Where(p => p.Vrsta == vrsta);
            ViewBag.Vrsta = vrsta;
            return View(await proizvodi.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = HttpContext.Session.GetInt32("KorisnikId");
            if (userId == null)
            {
                TempData["Msg"] = "Prvo unesi korisnika.";
                return RedirectToAction("Create", "Korisnici");
            }

            var cart = await _ctx.Kosarice
                .FirstOrDefaultAsync(k => k.ID_korisnika == userId && k.Status == "aktivna");

            if (cart == null)
            {
                cart = new Models.Kosarica { ID_korisnika = userId.Value, Status = "aktivna" };
                _ctx.Kosarice.Add(cart);
                await _ctx.SaveChangesAsync();
            }

            var proizvod = await _ctx.Proizvodi.FindAsync(id);
            if (proizvod == null) return NotFound();

            var stavka = await _ctx.StavkeKosarice
                .FirstOrDefaultAsync(s => s.ID_kosarice == cart.ID_kosarice && s.ID_proizvoda == id);

            if (stavka == null)
            {
                _ctx.StavkeKosarice.Add(new Models.StavkaKosarice
                {
                    ID_kosarice = cart.ID_kosarice,
                    ID_proizvoda = id,
                    Kolicina = 1,
                    CijenaKom = proizvod.Cijena
                });
            }
            else
            {
                stavka.Kolicina += 1;
            }
            await _ctx.SaveChangesAsync();
            TempData["Msg"] = "Dodano u košaricu.";
            return RedirectToAction("Index");
        }
    }
}
