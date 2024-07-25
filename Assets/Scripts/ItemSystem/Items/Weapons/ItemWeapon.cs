using System;
using ItemSystem.Items.Equipments;
using Player.EquipmentInventores;
using Shooting.Weapons;
using UnityEngine;

namespace ItemSystem.Item
{
    [Serializable]
    public class ItemWeapon : ItemEquipment
    {
        [field: SerializeField] private BaseWeapon _weapon;

        public BaseWeapon Weapon => _weapon;

        protected override ItemType GetItemType() =>
          ItemType.Equipment;
    }
}
