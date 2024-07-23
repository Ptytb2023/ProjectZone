using ItemSystem.Items.Equipments;
using ReactivePropertes;

namespace Player.EquipmentInventores.Slot
{
    public interface IInventoryEquipmentSlot
    {
        bool isEmpty { get; }
        string ItemID { get; }
        EquipmentType Type { get; }
    }
}