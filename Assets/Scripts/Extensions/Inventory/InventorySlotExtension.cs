using Inventarys.Data;
using Inventorys.Slot;

namespace Extensions.Inventory
{
    public static class InventorySlotExtension
    {
        public static bool CanPlaceItemInSlot(this InventorySlot inventorySlot, IInventoryItem item) =>
      (inventorySlot.ItemId.GetValue() == item.Id && inventorySlot.Amount.GetValue() < item.MaxStack)
      || inventorySlot.IsEmpty;

        public static int CalculateMaxAddableQuantity(this InventorySlot slot, IInventoryItem item) =>
            slot.IsEmpty ? item.MaxStack : item.MaxStack - slot.Amount.GetValue();
    }
}
