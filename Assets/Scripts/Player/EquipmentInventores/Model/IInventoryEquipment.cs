using EquipmentInventores.Structures;
using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Slot;

namespace Player.EquipmentInventores.Model
{
    public interface IInventoryEquipment
    {
        IInventoryEquipmentSlot[] EquipmentsSlots { get; }

        ReplaceItemResult ReplaceItem(EquipmentType type, string itemId);
    }
}