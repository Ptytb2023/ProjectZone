using Inventarys.Data;
using Inventorys.Structures;

namespace Inventarys
{
    public interface IInventory :  IExpandableInventory , IReadOnlyInventory
    {
        bool TryGetSlotIndexByItemId(string itemId, out int slotIndex);
    }

    public interface IExpandableInventory : IItemAdder, IItemRemover
    {
    }

    public interface IItemRemover
    {
        RemoveItemResult RemoveItem(int slotIndex, int count);
        RemoveItemResult RemoveItem(InventoryItem item, int count);
    }

    public interface IItemAdder
    {
        AddItemsResult AddItem(int slotIndex, InventoryItem item, int count);
        AddItemsResult AddItem(InventoryItem item, int count);
    }
}