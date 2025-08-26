using FineSelections.Web.Data;
using FineSelections.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Web.Controllers;

public class OrdersController : Controller
{
    private readonly WebshopDbContext _db;
    public OrdersController(WebshopDbContext db) => _db = db;

    private int GetKorisnikId() => 1; // demo

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var cart = await _db.Kosarice.Include(c => c.Stavke)
            .FirstOrDefaultAsync(c => c.ID_korisnika == GetKorisnikId() && c.status == "aktivna");
        if (cart is null || cart.Stavke.Count == 0) return RedirectToAction("Index", "Cart");

        var order = new Narudzba
        {
            ID_korisnika = GetKorisnikId(),
            datum_narudzbe = DateTime.UtcNow,
            status = "u obradi",
            ukupna_cijena = cart.Stavke.Sum(s => s.kolicina * s.cijena_kom)
        };
        _db.Narudzbe.Add(order);
        await _db.SaveChangesAsync();

        foreach (var s in cart.Stavke)
        {
            _db.StavkeNarudzbe.Add(new StavkaNarudzbe {
                ID_narudzbe = order.ID_narudzbe,
                ID_proizvoda = s.ID_proizvoda,
                kolicina = s.kolicina,
                cijena_kom = s.cijena_kom
            });
        }
        cart.status = "narucena";
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Thanks), new { id = order.ID_narudzbe });
    }

    public async Task<IActionResult> Thanks(int id)
    {
        var order = await _db.Narudzbe.FindAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }
}
