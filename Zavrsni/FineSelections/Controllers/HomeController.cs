
using Microsoft.AspNetCore.Mvc;
using FineSelections.Data;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _ctx;
        public HomeController(AppDbContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Index(string? q, string? vrsta)
        {
            var query = _ctx.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(p => p.Naziv.Contains(q) || (p.Opis ?? "").Contains(q));
            if (!string.IsNullOrWhiteSpace(vrsta))
                query = query.Where(p => p.Vrsta == vrsta);

            var proizvodi = await query.OrderBy(p => p.Naziv).Take(24).ToListAsync();
            ViewBag.Vrste = await _ctx.Products.Select(p => p.Vrsta).Distinct().OrderBy(v => v).ToListAsync();
            return View(proizvodi);
        }
    }
}
