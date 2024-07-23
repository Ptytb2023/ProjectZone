using ItemSystem.Items.Equipments;

namespace Player.EquipmentInventores
{
    public interface IInventoryEquipmentView : IEuipmentView
    {
    }

    public interface IEuipmentView
    {
        void SetIcon(ItemEquipment item);
        void ResetAllSlots();
    }
}