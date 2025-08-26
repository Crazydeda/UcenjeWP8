
using FineSelections.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FineSelections.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _ctx;
        public CartController(AppDbContext ctx) => _ctx = ctx;

        private Dictionary<int,int> GetCart()
        {
            var json = HttpContext.Session.GetString("cart");
            return json is null ? new() : JsonSerializer.Deserialize<Dictionary<int,int>>(json)!;
        }
        private void SaveCart(Dictionary<int,int> cart)
        {
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(int id, int qty = 1)
        {
            var cart = GetCart();
            cart[id] = cart.ContainsKey(id) ? cart[id] + qty : qty;
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            if (cart.ContainsKey(id)) cart.Remove(id);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            SaveCart(new());
            return RedirectToAction("Index");
        }
    }
}
