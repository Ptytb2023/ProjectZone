using System;
using Inventorys.Slot;
using Inventorys.Structures;

namespace Inventarys
{
    public interface IReadOnlyInventory
    {
        int Size { get; }

        event Action<string,int> ItemAdded;
        event Action<string,int> ItemRemoved;
        event Action<int> SizeChanged;

        IReadOnlyInventorySlot[] InventorySlots { get; }

        bool HasItem(string itemId);
        int GetItemAmount(string itemId);
    }
}
