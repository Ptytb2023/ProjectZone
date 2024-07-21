using ItemSystem;
using UnityEngine;

namespace Services
{
    public interface IItemService : IService
    {
        bool ContainsItem(string itemId);
        IItem GetItem(string itemId);
        bool TryUseItem(string itemId, GameObject gameObject);
    }
}

