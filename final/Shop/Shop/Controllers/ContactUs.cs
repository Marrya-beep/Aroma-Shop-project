using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    public class ContactUs : Controller
    {
        private readonly ShopDbContext _context;
        private readonly PermissionService _permissionService;

        public ContactUs(ShopDbContext context, PermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }

        // فقط Admin یا کاربر لاگین کرده می‌تونه فرم ببینه (GET)
        [HttpGet]
        public IActionResult NewMessage()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // فقط Admin دسترسی به GET داره، User عادی نداره
            if (!_permissionService.UserHasPermission(userId.Value, "ContactUS-Get"))
                return Unauthorized();

            return View();
        }

        // POST پیام برای همه کاربرها (User و Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewMessage(ContactMessage model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // چک پرمیشن POST
            if (!_permissionService.UserHasPermission(userId.Value, "ContactUs-Post"))
                return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Message.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "پیام شما با موفقیت ارسال شد!";
                return RedirectToAction("NewMessage");
            }

            return View(model);
        }

        // این تست ذخیره رو می‌تونیم حذف کنیم یا فقط Admin دسترسی داشته باشه
        public IActionResult TestSave()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_permissionService.UserHasPermission(userId.Value, "ContactUs-Post"))
                return Unauthorized();

            var msg = new ContactMessage
            {
                UserName = "TEST",
                Phone = "123",
                Email = "test@test.com",
                Message = "HELLO"
            };

            _context.Message.Add(msg);
            _context.SaveChanges();

            return Content("SAVED");
        }
    }
}

