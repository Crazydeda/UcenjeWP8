
using FineSelections.Data;
using FineSelections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineSelections.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _ctx;
        public ProductsController(AppDbContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Details(int id)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p is null) return NotFound();
            return View(p);
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Admin()
        {
            var items = await _ctx.Products.OrderBy(p => p.Naziv).ToListAsync();
            return View(items);
        }

        [Authorize(Roles="Admin")]
        public IActionResult Create() => View(new Product());

        [Authorize(Roles="Admin"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            if (!ModelState.IsValid) return View(p);
            _ctx.Add(p); await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p is null) return NotFound();
            return View(p);
        }

        [Authorize(Roles="Admin"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product p)
        {
            if (!ModelState.IsValid) return View(p);
            _ctx.Update(p); await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p is null) return NotFound();
            return View(p);
        }

        [Authorize(Roles="Admin"), HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _ctx.Products.FindAsync(id);
            if (p is not null)
            {
                _ctx.Remove(p); await _ctx.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Admin));
        }
    }
}
