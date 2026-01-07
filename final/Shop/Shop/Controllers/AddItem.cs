using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.Services;
using System.Linq;

namespace Shop.Controllers
{
    public class AddItemController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly PermissionService _permissionService;

        public AddItemController(ShopDbContext context, PermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }

        // GET: NewItem
        [HttpGet]
        public IActionResult NewItem()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (!_permissionService.UserHasPermission(userId.Value, "Add-Get"))
                return Unauthorized();

            return View();
        }

        // POST: NewItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewItem(string categoryName, ShopItem item)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (!_permissionService.UserHasPermission(userId.Value, "Add-Post"))
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(categoryName))
                return Json(new { success = false, errors = new[] { "نام دسته‌بندی الزامی است." } });

            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                category = new Category { Name = categoryName };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            item.IdCategory = category.Id;

            // اینجا فقط Validate بقیه فیلدها
            if (string.IsNullOrWhiteSpace(item.Name))
                return Json(new { success = false, errors = new[] { "نام محصول الزامی است." } });

            _context.ShopItems.Add(item);
            _context.SaveChanges();

            return Json(new { success = true, message = "محصول با موفقیت ذخیره شد!", item });
        }

    }
}

