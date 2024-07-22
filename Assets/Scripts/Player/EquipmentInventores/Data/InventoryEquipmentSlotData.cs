using ItemSystem.Items.Equipments;
using System;

namespace Player.EquipmentInventores.Data
{
    [Serializable]
    public class InventoryEquipmentSlotData
    {
        public string IdItem { get; private set; }

        public InventoryEquipmentSlotData(string idItem) => 
            IdItem = idItem;
    }
}
