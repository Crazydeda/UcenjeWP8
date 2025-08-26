using FineSelections.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Web.Controllers;

public class HomeController : Controller
{
    private readonly WebshopDbContext _db;
    public HomeController(WebshopDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        // "Novo u ponudi": zadnjih 8 proizvoda po ID-u
        var latest = await _db.Proizvodi.OrderByDescending(p => p.ID_proizvoda).Take(8).ToListAsync();
        ViewBag.Latest = latest;

        // "Kolekcije": uzmi po jednoj vrsti
        var collections = await _db.Proizvodi
            .GroupBy(p => p.vrsta)
            .Select(g => new { vrsta = g.Key!, sample = g.FirstOrDefault()! })
            .Take(6).ToListAsync();
        ViewBag.Collections = collections;
        return View();
    }
}
