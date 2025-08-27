using FineSelections.Data;
using FineSelections.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Controllers
{
    public class KosaricaController : Controller
    {
        private readonly WebshopContext _ctx;
        public KosaricaController(WebshopContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("KorisnikId");
            if (userId == null) return RedirectToAction("Create", "Korisnici");

            var cart = await _ctx.Kosarice
                .Include(k => k.Stavke)!.ThenInclude(s => s.Proizvod)
                .FirstOrDefaultAsync(k => k.ID_korisnika == userId && k.Status == "aktivna");

            if (cart == null)
            {
                cart = new Kosarica { ID_korisnika = userId.Value, Status = "aktivna" };
                _ctx.Kosarice.Add(cart);
                await _ctx.SaveChangesAsync();
                cart.Stavke = new List<StavkaKosarice>();
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Plus(int stavkaId)
        {
            var s = await _ctx.StavkeKosarice.FindAsync(stavkaId);
            if (s != null) s.Kolicina += 1;
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Minus(int stavkaId)
        {
            var s = await _ctx.StavkeKosarice.FindAsync(stavkaId);
            if (s != null)
            {
                s.Kolicina -= 1;
                if (s.Kolicina <= 0) _ctx.StavkeKosarice.Remove(s);
                await _ctx.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int stavkaId)
        {
            var s = await _ctx.StavkeKosarice.FindAsync(stavkaId);
            if (s != null) _ctx.StavkeKosarice.Remove(s);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = HttpContext.Session.GetInt32("KorisnikId");
            if (userId == null) return RedirectToAction("Create", "Korisnici");

            var cart = await _ctx.Kosarice
                .Include(k => k.Stavke)!.ThenInclude(s => s.Proizvod)
                .FirstOrDefaultAsync(k => k.ID_korisnika == userId && k.Status == "aktivna");

            if (cart == null || cart.Stavke == null || !cart.Stavke.Any())
            {
                TempData["Msg"] = "Košarica je prazna.";
                return RedirectToAction(nameof(Index));
            }

            var narudzba = new Narudzba
            {
                ID_korisnika = userId.Value,
                Status = "u obradi",
                UkupnaCijena = 0
            };
            _ctx.Narudzbe.Add(narudzba);
            await _ctx.SaveChangesAsync();

            decimal total = 0;
            foreach (var s in cart.Stavke)
            {
                total += s.CijenaKom * s.Kolicina;
                _ctx.StavkeNarudzbe.Add(new StavkaNarudzbe
                {
                    ID_narudzbe = narudzba.ID_narudzbe,
                    ID_proizvoda = s.ID_proizvoda,
                    Kolicina = s.Kolicina,
                    CijenaKom = s.CijenaKom
                });

                if (s.Proizvod != null)
                {
                    s.Proizvod.Zaliha = Math.Max(0, s.Proizvod.Zaliha - s.Kolicina);
                }
            }

            narudzba.UkupnaCijena = total;
            cart.Status = "naručena";
            _ctx.StavkeKosarice.RemoveRange(cart.Stavke);
            await _ctx.SaveChangesAsync();

            TempData["Msg"] = "Narudžba je kreirana.";
            return RedirectToAction("Index", "Narudzbe");
        }
    }
}
