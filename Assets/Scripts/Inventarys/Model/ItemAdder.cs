using Extensions.Inventory;
using Inventarys.Data;
using Inventorys.Slot;
using Inventorys.Structures;
using System.Collections.Generic;
using UnityEngine;

namespace Inventarys.Model
{
    public class ItemAdder : IItemAdder
    {
        private readonly Dictionary<int, InventorySlot> _slotBySlotId;

        public ItemAdder(Dictionary<int, InventorySlot> slotBySlotId) =>
            _slotBySlotId = slotBySlotId;

        public AddItemsResult AddItem(int slotIndex, IInventoryItem item, int amount)
        {
            if (!CanStackItemInSlot(slotIndex, item, amount))
                return CreateAddItemsResult(item.Id, amount, 0);

            InventorySlot slot = _slotBySlotId[slotIndex];

            return AddItem(slot, item, amount);
        }

        public AddItemsResult AddItem(IInventoryItem item, int amount)
        {
            int remainingAmountToAdd = amount;
            int totalAdded = 0;

            foreach (var (slotId, slot) in _slotBySlotId)
            {
                if (!slot.CanPlaceItemInSlot(item))
                    continue;

                var addItemResult = AddItem(slot, item, remainingAmountToAdd);

                remainingAmountToAdd -= addItemResult.AmountAdded;
                totalAdded += addItemResult.AmountAdded;

                if (remainingAmountToAdd <= 0)
                    break;
            }

            return CreateAddItemsResult(item.Id, amount, totalAdded);
        }

        private AddItemsResult AddItem(InventorySlot slot, IInventoryItem item, int amountToAdd)
        {
            if (slot.IsEmpty)
                slot.SetItemId(item.Id);

            int currentAmount = slot.Amount.GetValue();
            int maxAddableAmount = slot.CalculateMaxAddableQuantity(item);

            int actualAmountToAdd = Mathf.Clamp(amountToAdd, 0, maxAddableAmount);
            slot.SetAmount(currentAmount + actualAmountToAdd);

            return CreateAddItemsResult(item.Id, amountToAdd, actualAmountToAdd);
        }

        private bool CanStackItemInSlot(int slotIndex, IInventoryItem item, int amount) =>
        IsSlotValid(slotIndex) && item.IsStackable
              && _slotBySlotId[slotIndex].Amount.GetValue() < item.MaxStack;

        private bool IsSlotValid(int slotIndex) =>
          _slotBySlotId.ContainsKey(slotIndex);

        private AddItemsResult CreateAddItemsResult(string itemID, int requestedCount, int countAdded) =>
            new AddItemsResult(itemID, requestedCount, countAdded);
    }
}