using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Model;
using UnityEngine;

namespace Player.EquipmentInventores
{
    public interface IInventoryEquipmentView
    {
        void Init(IInventoryEquipment inventoryEquipment);
        void ResetAllIcon();
        void SetIcon(EquipmentType type, Sprite icon);
    }
}