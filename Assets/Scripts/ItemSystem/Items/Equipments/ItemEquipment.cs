using System;
using UnityEngine;

namespace ItemSystem.Items.Equipments
{
    [Serializable]
    public class ItemEquipment : UsableItem
    {
        [field: SerializeField] public EquipmentType EquipmentType { get; private set; }

        protected override ItemType GetItemType() =>
            ItemType.Equipment;
    }
}
