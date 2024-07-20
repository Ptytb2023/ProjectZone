using System;
using Inventarys.Data;
using Inventorys.Slot;
using Inventorys.Structures;
using System.Collections.Generic;

namespace Inventarys.Model
{
    public class ItemRemover : IItemRemover
    {
        private readonly Dictionary<int, InventorySlot> _slotBySlotId;

        public ItemRemover(Dictionary<int, InventorySlot> slotBySlotId) =>
            _slotBySlotId = slotBySlotId;


        public RemoveItemResult RemoveItem(int slotIndex, int count)
        {
            if (!TryGetSlot(slotIndex, out var slot))
                return CreateResultRemove(string.Empty, 0, false);

            if (!CanRemoveItem(slot, count))
                return CreateResultRemove(string.Empty, 0, false);

            DecreaseAmountInSlot(slot, count);

            return CreateResultRemove(slot.ItemId.GetValue(), count, true);
        }

        public RemoveItemResult RemoveItem(InventoryItem item, int count)
        {
            int totalRemoved = 0;
            string itemId = item.Id;

            List<Action> removed = new List<Action>();

            foreach (var slot in _slotBySlotId.Values)
            {
                if (!(slot.ItemId.GetValue() == itemId))
                    continue;

                int amountToRemove = Math.Min(slot.Amount.GetValue(), count - totalRemoved);
                if (amountToRemove > 0)
                {
                    removed.Add(() => DecreaseAmountInSlot(slot, amountToRemove));
                    totalRemoved += amountToRemove;

                    if (totalRemoved >= count)
                        break;
                }
            }

            bool isSuccess = totalRemoved >= count;

            if (isSuccess)
                removed.ForEach(x => x.Invoke());

            return CreateResultRemove(item.Id, totalRemoved, isSuccess);
        }

        private bool TryGetSlot(int slotIndex, out InventorySlot slot) =>
         _slotBySlotId.TryGetValue(slotIndex, out slot);

        private bool CanRemoveItem(InventorySlot slot, int count) =>
            slot.Amount.GetValue() >= count;

        private void DecreaseAmountInSlot(InventorySlot slot, int count)
        {
            var newAmount = slot.Amount.GetValue() - count;
            slot.SetAmount(newAmount);
        }

        private RemoveItemResult CreateResultRemove(string itemId, int amount, bool success) =>
            new RemoveItemResult(itemId, amount, success);
    }
}