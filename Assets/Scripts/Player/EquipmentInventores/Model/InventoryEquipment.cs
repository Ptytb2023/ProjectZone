using Inventarys;
using Inventarys.Model;
using ItemSystem.Items.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Player.EquipmentInventores.Model
{
    public class InventoryEquipment : IInventoryEquipment
    {
        private InventoryController _inventory;
        private Dictionary<EquipmentType, ItemEquipment> _slots;

        public ItemEquipment[] ItemEquipments => _slots.Values.ToArray();

        public event Action<EquipmentType, ItemEquipment> ChangedEquipment;

        public InventoryEquipment(List<ItemEquipment> data)
        {
            _slots = new Dictionary<EquipmentType, ItemEquipment>();

            foreach (var item in data)
                _slots.Add(item.EquipmentType, item);
        }

        public void Initilize(InventoryController inventoryController) =>
            _inventory = inventoryController;

        public bool TryСlothe(ItemEquipment item)
        {
            var result = _inventory.AddItem(item);

            if (result.AmountAdded <= 0)
                return false;

            EquipmentType type = item.EquipmentType;
            _slots[type] = item;

            ChangedEquipment?.Invoke(type, item);

            return true;
        }

    }
}
