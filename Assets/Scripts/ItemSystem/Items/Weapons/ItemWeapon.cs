using ItemSystem.Items;
using Shooting;
using Shooting.Weapons;
using System;
using UnityEngine;

namespace ItemSystem.Item
{
    [Serializable]
    public class ItemWeapon : UsableItem, IUsabelItem
    {
        [field: SerializeField] private BaseWeapon _weapon;

        public IWeapon Weapon => _weapon;

        public override bool TryUseItem(GameObject target)
        {
            if (!target.TryGetComponent<IWeaponSystem>(out var weaponSystem))
                return false;

            weaponSystem.EquipWeapon(Weapon);

            return true;
        }

        protected override ItemType GetItemType() =>
          ItemType.Weapons;
    }
}
