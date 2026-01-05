using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class ItemDetail : Controller
    {
        private readonly ShopDbContext _context;
        public ItemDetail(ShopDbContext context)
        {
            _context = context;
        }
        public IActionResult ItemDetails(int id)
        {
            var product = _context.ShopItems
         .FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();
            var category = _context.Categories
                .FirstOrDefault(c => c.Id == product.IdCategory);

            ViewBag.CategoryName = category?.Name ?? "نامشخص";

            return View(product);
        }
    }
}
