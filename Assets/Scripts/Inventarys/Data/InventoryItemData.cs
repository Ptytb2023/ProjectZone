using System;

namespace Inventarys.Data
{
    [Serializable]
    public class InventoryItem
    {
        public string Id { get; }
        public bool IsStackable { get; }
        public int MaxStack { get; }

        public InventoryItem(string id, bool isStackable, int maxStack)
        {
            Id = id;
            IsStackable = isStackable;
            MaxStack = maxStack;
        }
    }
}
