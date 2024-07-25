using ItemSystem;
using UnityEngine;

namespace Services
{
    public interface IItemService : IService
    {
        bool ContainsItem(string itemId);
        IItem GetItem(string itemId);
        T GetItem<T>(string itemId) where T : IItem;
        IItem GetRandomItem();
    }
}

