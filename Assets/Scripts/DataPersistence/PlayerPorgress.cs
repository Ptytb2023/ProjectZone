using Inventarys.Data;
using Player.EquipmentInventores.Data;
using System;
using System.Collections.Generic;

namespace DataPersistence
{
    [Serializable]
    public class PlayerPorgress
    {
        public InventoryData MainInvetory;
        public List<InventoryEquipmentSlotData> InventoryEquipments;

        public PlayerPorgress(InventoryData mainInvetory, List<InventoryEquipmentSlotData> inventoryEquipments)
        {
            MainInvetory = mainInvetory;
            InventoryEquipments = inventoryEquipments;
        }
    }
}
