using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.EquipmentInventores
{
    public class CharacterAppearanceController : MonoBehaviour, IEuipmentView
    {
        [SerializeField] private Transform _weaponPoint;
        [SerializeField] private List<CharacterEquipmentSlot> _slots;

        private IInventoryEquipment _inventory;

        public Transform WeaponPoint => _weaponPoint;

        public void SetIcon(ItemEquipment item)
        {
            IEnumerable<CharacterEquipmentSlot> slots = _slots.Where(x => x.Type == item.EquipmentType);

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
