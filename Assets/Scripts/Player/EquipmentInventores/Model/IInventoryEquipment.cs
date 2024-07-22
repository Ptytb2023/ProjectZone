using Inventarys;
using ItemSystem.Items.Equipments;
using System;

namespace Player.EquipmentInventores.Model
{
    public interface IInventoryEquipment
    {
        ItemEquipment[] ItemEquipments { get; }

        event Action<EquipmentType, ItemEquipment> ChangedEquipment;

        bool TryСlothe(ItemEquipment item);
        void Initilize(InventoryController inventoryController);
    }
}