using System.Collections.Generic;
using System;

namespace Inventorys.Structures
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

        public int Remains => AmountToAdd - AmountAdded;

        public AddItemsResult(string itemId, int amountToAdd, int amountAdded)
        {
            if (amountAdded > amountToAdd)
                throw new
                    ArgumentOutOfRangeException($"" +
                    $"{nameof(amountAdded)} " +
                    $"must not be greater than " +
                    $"{nameof(amountToAdd)}");

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
}
