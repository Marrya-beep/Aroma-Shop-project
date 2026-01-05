using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("ShopItems")]
    public class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [Column("IdCategory")] 
        public int IdCategory { get; set; }
    }

}
