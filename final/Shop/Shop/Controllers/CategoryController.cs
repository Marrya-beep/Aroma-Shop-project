using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Services;
using System.Linq;

namespace Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly PermissionService _permissionService;

        public CategoryController(ShopDbContext context, PermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }

        public IActionResult Category()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_permissionService.UserHasPermission(userId.Value, "Category-Get"))
                return Unauthorized();

            var categories = _context.Categories
                .Select(c => c.Name)
                .ToList();

            return View(categories);
        }

        public IActionResult ProductsByCategory(string category)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_permissionService.UserHasPermission(userId.Value, "Category-Get"))
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(category))
                return RedirectToAction("Category");

            var categoryEntity = _context.Categories
                .FirstOrDefault(c => c.Name == category);

            if (categoryEntity == null)
                return RedirectToAction("Category");

            var products = _context.ShopItems
                .Where(i => i.IdCategory == categoryEntity.Id)
                .ToList();

            ViewBag.CategoryName = category;
            return View(products);
        }
    }
}

