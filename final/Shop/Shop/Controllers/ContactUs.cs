using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class ContactUs : Controller
    {
        private readonly ShopDbContext _context;

        public ContactUs(ShopDbContext context)
        {
            _context = context;
        }
        public IActionResult TestSave()
        {
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

        [HttpGet]
        public IActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewMessage(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _context.Message.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "پیام شما با موفقیت ارسال شد!";
                return RedirectToAction("NewMessage");
            }

            return View(model);
        }

    }
}

