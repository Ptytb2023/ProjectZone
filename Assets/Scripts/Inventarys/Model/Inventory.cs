﻿using System;
using Inventarys.Data;
using Inventorys.Slot;
using Inventorys.Structures;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Inventarys.Model
{
    public class Inventory : IInventory
    {
        private readonly Dictionary<int, InventorySlot> _slots;
        private readonly IItemAdder _itemAdder;
        private readonly IItemRemover _itemRemover;

        private readonly int _size;

        public int Size => _size;
        public IReadOnlyInventorySlot[] InventorySlots => _slots.Values.ToArray();

        public event Action<string, int> ItemAdded;
        public event Action<string, int> ItemRemoved;


        public Inventory(InventoryData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Slots == null) throw new ArgumentNullException(nameof(data.Slots));
            if (data.Slots.Count > data.Size) throw new ArgumentOutOfRangeException(nameof(data.Slots));

            _slots = new Dictionary<int, InventorySlot>();
            _size = data.Size;

            CreateSlots(data.Slots);

            _itemAdder = new ItemAdder(_slots);
            _itemRemover = new ItemRemover(_slots);
        }

        private void CreateSlots(List<InventorySlotData> slots)
        {
            for (int i = 0; i < slots.Count; i++)
                _slots[i] = new InventorySlot(slots[i]);

            for (int i = slots.Count; i < _size; i++)
                _slots[i] = new InventorySlot();
        }

        public int GetItemAmount(string itemId) =>
            ContainsItem(itemId) ? CalculateTotalItemAmount(itemId) : 0;

        private int CalculateTotalItemAmount(string itemId) =>
            InventorySlots
                .Where(slot => slot.ItemId.GetValue() == itemId)
                .Sum(slot => slot.Amount.GetValue());

        public bool ContainsItem(string itemId) =>
            InventorySlots.Any(x => x.ItemId.GetValue() == itemId);

        public string GetItemIdInSlot(int idSlot) =>
            _slots[idSlot].ItemId.GetValue();

        public bool TryGetSlotIndexByItemId(string itemId, out int slotId)
        {
            foreach (var (id, slot) in _slots)
            {
                if (slot.ItemId.GetValue() == itemId)
                {
                    slotId = id;
                    return true;
                }
            }

            slotId = -1;
            return false;
        }

        public AddItemsResult AddItem(int slotId, IInventoryItem item, int count = 1) =>
            AddItemInternal(slotId, item, count);

        public AddItemsResult AddItem(IInventoryItem item, int count = 1) =>
            AddItemInternal(null, item, count);

        private AddItemsResult AddItemInternal(int? slotId, IInventoryItem item, int count = 1)
        {
            var result = slotId.HasValue
                ? _itemAdder.AddItem(slotId.Value, item, count)
                : _itemAdder.AddItem(item, count);

            if (result.AmountAdded > 0)
                TriggerItemAddedEvent(item, result);

            return result;
        }

        public RemoveItemResult RemoveItem(IInventoryItem item, int count = 1) =>
            RemoveItemIterval(null, count, item);

        public RemoveItemResult RemoveItem(int slotId, int count = 1) =>
            RemoveItemIterval(slotId, count, null);

        private RemoveItemResult RemoveItemIterval(int? slotIndex, int count, IInventoryItem item)
        {
            var result = slotIndex.HasValue
                ? _itemRemover.RemoveItem(slotIndex.Value, count)
                : _itemRemover.RemoveItem(item, count);

            //if (result.Success)
            //    TriggerItemRemoveEvent(item.Id, result);

            return result;
        }

        private void TriggerItemAddedEvent(IInventoryItem item, AddItemsResult result) =>
          ItemAdded?.Invoke(item.Id, result.AmountAdded);

        private void TriggerItemRemoveEvent(string itemId, RemoveItemResult result) =>
            ItemRemoved?.Invoke(itemId, result.AmountRemoved);
    }
}
