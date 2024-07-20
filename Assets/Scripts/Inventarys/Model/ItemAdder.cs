﻿using Inventarys.Data;
using Inventorys.Slot;
using Inventorys.Structures;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventarys.Model
{
    public class ItemAdder : IItemAdder
    {
        private readonly Dictionary<int, InventorySlot> _slotBySlotId;

        public ItemAdder(Dictionary<int, InventorySlot> slotBySlotId) =>
            _slotBySlotId = slotBySlotId;


        public AddItemsResult AddItem(int slotIndex, InventoryItem item, int amount)
        {
            if (!CanStackItemInSlot(slotIndex, item, amount))
                return CreateAddItemsResult(item.Id, amount, 0);

            InventorySlot slot = _slotBySlotId[slotIndex];
            int currentAmount = slot.Amount.GetValue();
            int countAdded = Mathf.Clamp(currentAmount + amount, 0, item.MaxStack);

            slot.SetAmount(countAdded);

            return CreateAddItemsResult(item.Id, amount, countAdded);
        }

        public AddItemsResult AddItem(InventoryItem item, int quantity)
        {
            int remainingQuantity = quantity;
            int totalAdded = 0;

            foreach (var (slotId, inventorySlot) in _slotBySlotId)
            {
                if (CanPlaceItemInSlot(item, inventorySlot))
                    continue;

                var addItemResult = AddItem(slotId, item, remainingQuantity);

                remainingQuantity -= addItemResult.Remains;
                totalAdded += addItemResult.AmountAdded;

                if (remainingQuantity <= 0)
                    break;
            }

            return CreateAddItemsResult(item.Id, quantity, totalAdded);
        }

        private bool CanStackItemInSlot(int slotIndex, InventoryItem item, int amount) =>
          IsSlotValid(slotIndex) && item.IsStackable
                && _slotBySlotId[slotIndex].Amount.GetValue() < item.MaxStack;

        private bool IsSlotValid(int slotIndex) =>
          _slotBySlotId.ContainsKey(slotIndex);

        private static bool CanPlaceItemInSlot(InventoryItem item, InventorySlot inventorySlot) =>
            inventorySlot.ItemId.GetValue() != item.Id || inventorySlot.IsEmpty;

        private AddItemsResult CreateAddItemsResult(string itemID, int requestedCount, int countAdded) =>
            new AddItemsResult(itemID, requestedCount, countAdded);
    }
}