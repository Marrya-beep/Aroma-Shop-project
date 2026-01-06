using Microsoft.AspNetCore.Mvc;
using Shop.Data;

namespace Shop.Controllers.API
{
    [ApiController]
    [Route("api/category")]
    public class CategoryAPIController : Controller
    {

        private readonly ShopDbContext _context;

        public CategoryAPIController(ShopDbContext context)
        {
            _context = context;
        }
        [HttpGet("category-list")]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories
                .Select(c => c.Name)
                .ToList();

            return Ok(categories);
        }

    }
}
