using Shop.Models;

namespace Shop.Data
{
    public class ItemsRepository
    {
        public static List<ShopItem> Items { get; } = new List<ShopItem>();
        private static int _nextId = 1;

        public static void AddItem(ShopItem item)
        {
            item.Id = _nextId++;
            Items.Add(item);
        }
    }
}
