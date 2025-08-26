using FineSelections.Web.Data;
using FineSelections.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Web.Controllers;

public class CartController : Controller
{
    private readonly WebshopDbContext _db;
    public CartController(WebshopDbContext db) => _db = db;

    private async Task<Kosarica> GetOrCreateCartAsync(int korisnikId)
    {
        var cart = await _db.Kosarice.Include(c => c.Stavke)
            .FirstOrDefaultAsync(c => c.ID_korisnika == korisnikId && c.status == "aktivna");
        if (cart is null)
        {
            cart = new Kosarica { ID_korisnika = korisnikId, datum_kreiranja = DateTime.UtcNow, status = "aktivna" };
            _db.Kosarice.Add(cart);
            await _db.SaveChangesAsync();
        }
        return cart;
    }

    // Demo: koristimo korisnikId=1 jer nema login sustava
    private int GetKorisnikId() => 1;

    public async Task<IActionResult> Index()
    {
        var cart = await GetOrCreateCartAsync(GetKorisnikId());
        await _db.Entry(cart).Collection(c => c.Stavke).LoadAsync();
        var items = await _db.StavkeKosarice.Where(s => s.ID_kosarice == cart.ID_kosarice).ToListAsync();
        ViewBag.Proizvodi = await _db.Proizvodi.ToDictionaryAsync(p => p.ID_proizvoda, p => p);
        return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int productId, int qty = 1)
    {
        var p = await _db.Proizvodi.FindAsync(productId);
        if (p is null) return NotFound();
        var cart = await GetOrCreateCartAsync(GetKorisnikId());
        var existing = await _db.StavkeKosarice.FirstOrDefaultAsync(s => s.ID_kosarice == cart.ID_kosarice && s.ID_proizvoda == productId);
        if (existing is null)
        {
            _db.StavkeKosarice.Add(new StavkaKosarice {
                ID_kosarice = cart.ID_kosarice,
                ID_proizvoda = productId,
                kolicina = qty,
                cijena_kom = p.cijena
            });
        }
        else
        {
            existing.kolicina += qty;
        }
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var item = await _db.StavkeKosarice.FindAsync(id);
        if (item != null)
        {
            _db.StavkeKosarice.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
