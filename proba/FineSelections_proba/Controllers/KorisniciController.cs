using FineSelections.Data;
using FineSelections.Models;
using Microsoft.AspNetCore.Mvc;

namespace FineSelections.Controllers
{
    public class KorisniciController : Controller
    {
        private readonly WebshopContext _ctx;
        public KorisniciController(WebshopContext ctx) => _ctx = ctx;

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Korisnik());
        }

        [HttpPost]
        public IActionResult Create(Korisnik model)
        {
            if (!ModelState.IsValid) return View(model);
            _ctx.Korisnici.Add(model);
            _ctx.SaveChanges();
            HttpContext.Session.SetInt32("KorisnikId", model.ID_korisnika);
            TempData["Msg"] = $"Dobrodošao/la, {model.Ime}!";
            return RedirectToAction("Index", "Home");
        }
    }
}
