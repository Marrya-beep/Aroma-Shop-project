using Microsoft.AspNetCore.Mvc;
using Shop.Data;

namespace Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopDbContext _context;
        public CategoryController(ShopDbContext context)
        {
            _context = context;
        }
        public IActionResult Category()
        {
            var categories = _context.Categories
                .Select(c => c.Name)
                .ToList();

            return View(categories);
        }
        public IActionResult ProductsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return RedirectToAction("Category");

            var categoryEntity = _context.Categories
                .FirstOrDefault(c => c.Name == category);

            if (categoryEntity == null)
                return RedirectToAction("Index");

            var products = _context.ShopItems
                .Where(i => i.IdCategory == categoryEntity.Id)
                .ToList();

            ViewBag.CategoryName = category;
            return View(products);
        }
    }
}
