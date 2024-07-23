using ItemSystem.Items.Equipments;

namespace Player.EquipmentInventores.Slot
{
    public class InventoryEquipmentSlot : IInventoryEquipmentSlot
    {
        private string _itemID;
        private EquipmentType _type;

        public string ItemID => _itemID;
        public EquipmentType Type => _type;
        public bool isEmpty => string.IsNullOrEmpty(_itemID);


        public InventoryEquipmentSlot(string itemID, EquipmentType type)
        {
            _itemID = itemID;
            _type = type;
        }

        public void SetSlot(string itemId) =>
            _itemID = itemId;
    }
}
