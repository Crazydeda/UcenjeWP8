using FineSelections.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Web.Controllers;

public class ProductsController : Controller
{
    private readonly WebshopDbContext _db;
    public ProductsController(WebshopDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? vrsta, string? q)
    {
        var query = _db.Proizvodi.AsQueryable();
        if (!string.IsNullOrWhiteSpace(vrsta)) query = query.Where(p => p.vrsta == vrsta);
        if (!string.IsNullOrWhiteSpace(q)) query = query.Where(p => (p.naziv ?? "").Contains(q) || (p.opis ?? "").Contains(q));
        var items = await query.OrderBy(p => p.naziv).ToListAsync();
        ViewBag.Vrste = await _db.Proizvodi.Select(p => p.vrsta!).Distinct().ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(int id)
    {
        var p = await _db.Proizvodi.FindAsync(id);
        if (p == null) return NotFound();
        return View(p);
    }
}
