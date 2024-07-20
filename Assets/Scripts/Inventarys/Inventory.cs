using System;
using Inventarys.Data;
using Inventorys.Slot;
using Inventorys.Structures;
using System.Collections.Generic;
using UnityEngine;

namespace Inventarys
{
    public class Inventory : IReadOnlyInventory
    {
        private Dictionary<string, InventorySlot> _slotByItemId;
        private Dictionary<int, InventorySlot> _slotBySlotId;

        private SizeInvetoryData _size;

        public IEnumerable<IReadOnlyInventorySlot> InventorySlots => _slotByItemId.Values;

        public event Action<AddItemsResult> ItemAdded;
        public event Action<RemoveItemResult> ItemRemoved;
        public event Action<SizeInvetoryData> SizeChanged;

        public Inventory(InvetoryData data)
        {
            _slotByItemId = new Dictionary<string, InventorySlot>();
            _size = data.SizeInvetory;
        }

        public int GetItemAmount(string itemId) =>
            HasItem(itemId) ? CalculateTotalItemAmount(itemId) : 0;

        public bool HasItem(string itemId) =>
            _slotByItemId.ContainsKey(itemId);

        public AddItemsResult AddItem(int slotId, InventoryItem item, int count)
        {
            if (item.IsStackable && IsSlotValid(slotId) == false)
                return CreateResultAdd(item.Id, count, 0);


            InventorySlot slot = _slotBySlotId[slotId];
            int currentAmount = slot.Amount.GetCurrentValue();
            int countAdded = Mathf.Clamp(currentAmount + count, 0, item.MaxStack);

            slot.SetAmount(countAdded);

            return CreateResultAdd(item.Id, count, countAdded, true);
        }

        public AddItemsResult AddItem(InventoryItem item, int count)
        {
            int requestedCount = count;
            int countAdded = 0;

            foreach ((int id, InventorySlot slot) in _slotBySlotId)
            {
                if ((slot.ItemId.GetCurrentValue() == item.Id || slot.IsEmpty) == false)
                    continue;

                var result = AddItem(id, item, requestedCount);
                requestedCount -= result.Remains;

                countAdded = result.AmountAdded;

                if (requestedCount <= 0)
                    break;
            }

            return CreateResultAdd(item.Id, count, countAdded);
        }

        public RemoveItemResult RemoveItem(int slotId, int count)
        {
            if (!IsSlotValid(slotId))
                return CreateResultRemove(string.Empty, 0, false);

            var slot = _slotBySlotId[slotId];
            int remains = slot.Amount.GetCurrentValue() - count;

            if (remains < 0)
                return CreateResultRemove(string.Empty, 0, false);

            slot.SetAmount(remains);

            return CreateResultRemove(slot.ItemId.GetCurrentValue(), count, true, true);
        }

        private int CalculateTotalItemAmount(string itemId)
        {
            int totalAmount = 0;

            foreach (var slot in InventorySlots)
                if (slot.ItemId.GetCurrentValue() == itemId)
                    totalAmount += slot.Amount.GetCurrentValue();

            return totalAmount;
        }


        private bool IsSlotValid(int slotId) =>
            _slotBySlotId.ContainsKey(slotId);

        private RemoveItemResult CreateResultRemove(string itemId, int amount, bool success,
            bool notify = false)
        {
            var result = new RemoveItemResult(itemId, amount, success);

            if (notify)
                ItemRemoved?.Invoke(result);

            return result;
        }

        private AddItemsResult CreateResultAdd(string itemID, int requestedCount, int countAdded,
            bool notify = false)
        {
            var result = new AddItemsResult(itemID, requestedCount, countAdded);

            if (notify)
                ItemAdded?.Invoke(result);

            return result;
        }
    }
}
