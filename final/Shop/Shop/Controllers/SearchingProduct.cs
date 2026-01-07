using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Services;
using System.Linq;

namespace Shop.Controllers
{
    public class SearchingProduct : Controller
    {
        private readonly ShopDbContext _context;
        private readonly PermissionService _permissionService;

        public SearchingProduct(ShopDbContext context, PermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_permissionService.UserHasPermission(userId.Value, "Search-Get"))
                return Unauthorized();

            var results = _context.ShopItems
                .Where(x => x.Name.Contains(query))
                .ToList();

            return View(results);
        }

        [HttpPost]
        public IActionResult SearchPost(string query)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_permissionService.UserHasPermission(userId.Value, "Search-Post"))
                return Unauthorized();

            var results = _context.ShopItems
                .Where(x => x.Name.Contains(query))
                .ToList();

            return View("Search", results);
        }
    }
}

