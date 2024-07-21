using ItemSystem;

namespace Services
{
    public interface IItemService : IService
    {
        bool ContainsItem(string itemId);
        IItem GetItem(string itemId);
    }
}

