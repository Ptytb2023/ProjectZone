using EquipmentInventores.Structures;
using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Data;
using Player.EquipmentInventores.Slot;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Player.EquipmentInventores.Model
{
    public class InventoryEquipment : IInventoryEquipment
    {
        private Dictionary<EquipmentType, InventoryEquipmentSlot> _slots;
        public IInventoryEquipmentSlot[] EquipmentsSlots => _slots.Values.ToArray();


        public InventoryEquipment(List<InventoryEquipmentSlotData> data)
        {
            _slots = new Dictionary<EquipmentType, InventoryEquipmentSlot>();

            foreach (var item in data)
            {
                if (_slots.ContainsKey(item.Type))
                    throw new ArgumentException();

                var slot = new InventoryEquipmentSlot(item.itemId, item.Type);
                _slots.Add(slot.Type, slot);
            }
        }

        public ReplaceItemResult ReplaceItem(EquipmentType type, string itemId)
        {
            if (!(_slots.TryGetValue(type, out var slot)))
                return new ReplaceItemResult(itemId, null, false);

            var itemIdRemoved = slot.ItemID;
            slot.SetSlot(itemId);

            return new ReplaceItemResult(itemIdRemoved, itemId, true);
        }
    }
}
