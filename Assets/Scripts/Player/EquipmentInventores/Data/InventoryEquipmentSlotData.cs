using System;
using ItemSystem.Items.Equipments;
using UnityEngine;

namespace Player.EquipmentInventores.Data
{
    [Serializable]
    public class InventoryEquipmentSlotData
    {
        [field: SerializeField] public string itemId { get; set; }
        [field: SerializeField] public EquipmentType Type { get; set; }
        public InventoryEquipmentSlotData(string idItem, EquipmentType type)
        {
            itemId = idItem;
            Type = type;
        }
    }
}
