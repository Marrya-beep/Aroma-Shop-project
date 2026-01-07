using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    [Table("ShopItems")]
    public class ShopItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام محصول الزامی است.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "قیمت محصول الزامی است.")]
        public int Price { get; set; }

        [Column("IdCategory")]
        public int IdCategory { get; set; }

        [ForeignKey(nameof(IdCategory))]
        public Category Category { get; set; }
    }
}
