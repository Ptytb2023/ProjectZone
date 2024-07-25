using Inventarys.Data;
using UnityEngine;

namespace ItemSystem
{
    public interface IItem : IInventoryItem
    {
        Sprite Icon { get; }
        string Name { get; }
        string Description { get; }
        ItemType Type { get; }
        public bool IsUsable { get; }
    }
}
