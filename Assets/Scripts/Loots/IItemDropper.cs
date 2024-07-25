using UnityEngine;

namespace Loots
{
    public interface IItemDropper
    {
        void DropItem(Transform dropper, string itemId);
        void DropItemForward(Transform dropper, string itemId);
    }
}