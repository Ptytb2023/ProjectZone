using System;
using Inventorys.Slot;
using System.Collections.Generic;
using Inventarys.Data;
using Inventorys.Structures;

namespace Inventarys
{
    public interface IReadOnlyInventory
    {
        event Action<AddItemsResult> ItemAdded;
        event Action<RemoveItemResult> ItemRemoved;
        event Action<SizeInvetoryData> SizeChanged;

        IEnumerable<IReadOnlyInventorySlot> InventorySlots { get; }

        bool HasItem(string itemId);
        int GetItemAmount(string itemId);
    }
}
