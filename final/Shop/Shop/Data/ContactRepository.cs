using Shop.Models;

namespace Shop.Data
{
    public class ContactRepository
    {
        public static List<ContactMessage> Messages = new();
        private static int _nextId = 1;

        public static void AddMessage(ContactMessage message)
        {
            message.Id = _nextId++;
            Messages.Add(message);
        }
    }
}
