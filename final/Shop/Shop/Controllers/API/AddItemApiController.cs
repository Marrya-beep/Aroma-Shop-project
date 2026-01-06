using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Models.Dtos;


namespace Shop.Controllers.API
{
    [ApiController]
    [Route("api")]
    public class AddItemApiController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public AddItemApiController(ShopDbContext context)
        {
            _context = context;
        }
        [HttpGet("Show-Item")]
        public IActionResult GetAll()
        {
            var items = _context.ShopItems
                .Include(x => x.Category)
                .Select(x => new ShopItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = x.Category.Name
                })
                .ToList();

            return Ok(items);
        }



        [HttpPost("Add-Item")]
        public IActionResult Add([FromBody] AddItemDto dto)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Name == dto.Category);

            if (category == null)
            {
                category = new Category { Name = dto.Category };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            var item = new ShopItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IdCategory = category.Id
            };

            _context.ShopItems.Add(item);
            _context.SaveChanges();

            return Ok("محصول اضافه شد");
        }

    }
}

