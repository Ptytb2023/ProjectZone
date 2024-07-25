using Inventarys.Data;
using Inventorys.Slot;

namespace Extensions.Inventory
{
    public static class InventorySlotExtension
    {
        public static bool CanPlaceItemInSlot(this InventorySlot slot, IInventoryItem item) =>
            slot.IsEmpty || (slot.ItemId.GetValue() == item.Id && item.IsStackable &&
            slot.Amount.GetValue() < item.MaxStack);


        public static int CalculateMaxAddableQuantity(this InventorySlot slot, IInventoryItem item) =>
            slot.IsEmpty ? item.MaxStack : item.MaxStack - slot.Amount.GetValue();
    }
}
