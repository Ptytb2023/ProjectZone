using ItemSystem.Items.Equipments;

namespace Player.EquipmentInventores
{
    public interface IInventoryEquipmentController
    {
        bool TrySetEquipment(ItemEquipment equipment);
    }
}