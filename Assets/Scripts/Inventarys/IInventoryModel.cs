using System.Collections.Generic;

namespace Inventorys
{
    public interface IInventoryModel
    {
        AddItemsResult AddItem(int slotId, string itemId);
        AddItemsResult AddItem(string itemId);
        RemoveItemResult RemoveItem(int slotId);
        bool HasItem(string itemId);
        int GetItemAmount(string itemId);
        InventoryStatus GetInventoryStatus();
    }

    public struct AddItemsResult
    {
        public readonly string ItemId;
        public readonly int AmountToAdd;
        public readonly int AmountAdded;

        public AddItemsResult(string itemId, int amountToAdd, int amountAdded)
        {
            ItemId = itemId;
            AmountToAdd = amountToAdd;
            AmountAdded = amountAdded;
        }
    }

    public struct RemoveItemResult
    {
        public readonly string ItemId;
        public readonly int AmountRemoved;
        public readonly bool Success;

        public RemoveItemResult(string itemId, int amountRemoved, bool success)
        {
            ItemId = itemId;
            AmountRemoved = amountRemoved;
            Success = success;
        }
    }

    public struct InventoryStatus
    {
        public readonly Dictionary<string, int> ItemCounts;

        public InventoryStatus(Dictionary<string, int> itemCounts)
        {
            ItemCounts = itemCounts;
        }
    }
}
