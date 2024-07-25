using ItemSystem.Items.Equipments;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.EquipmentInventores
{
    public class InventoryEquipmentView : MonoBehaviour, IInventoryEquipmentView
    {
        [SerializeField] private List<EquipmentItemSlot> _slots;

        public void SetIcon(ItemEquipment item)
        {
            IEnumerable<EquipmentItemSlot> slots = _slots.Where(x => x.Type == item.EquipmentType);

            foreach (var slot in slots)
                slot.SetIcon(item.Icon);
        }

        public void ResetAllSlots()
        {
            foreach (var slot in _slots)
                slot.ResetIcon();
        }
    }
}
