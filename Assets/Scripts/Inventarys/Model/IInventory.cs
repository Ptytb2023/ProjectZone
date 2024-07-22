using Inventarys.Data;
using Inventorys.Structures;

namespace Inventarys.Model
{
    public interface IInventory : IExpandableInventory, IReadOnlyInventory
    {
        bool TryGetSlotIndexByItemId(string itemId, out int slotIndex);
    }

    public interface IExpandableInventory : IItemAdder, IItemRemover { }

    public interface IItemRemover
    {
        RemoveItemResult RemoveItem(int slotIndex, int count = 1);
        RemoveItemResult RemoveItem(IInventoryItem item, int count = 1);
    }

    public interface IItemAdder
    {
        AddItemsResult AddItem(int slotIndex, IInventoryItem item, int count = 1);
        AddItemsResult AddItem(IInventoryItem item, int count = 1);
    }
}