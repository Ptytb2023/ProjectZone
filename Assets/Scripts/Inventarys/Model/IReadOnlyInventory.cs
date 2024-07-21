using System;
using Inventorys.Slot;
using Inventorys.Structures;

namespace Inventarys.Model
{
    public interface IReadOnlyInventory
    {
        int Size { get; }

        event Action<string,int> ItemAdded;
        event Action<string,int> ItemRemoved;

        IReadOnlyInventorySlot[] InventorySlots { get; }

        bool ContainsItem(string itemId);
        int GetItemAmount(string itemId);
    }
}
