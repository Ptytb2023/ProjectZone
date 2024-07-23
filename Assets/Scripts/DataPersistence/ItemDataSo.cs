using ItemSystem.Item;
using ItemSystem.Items;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ItemSystem;
using ItemSystem.Items.Equipments;

namespace DataPersistence
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/DataPersistence/ItemData", order = 51)]
    public class ItemDataSo : ScriptableObject
    {
        [SerializeField] private List<ItemResource> _itemResources;
        [SerializeField] public List<ItemWeapon> _itemWeapons;
        [SerializeField] public List<ItemEquipment> _itemEquipment;

        public IEnumerable<IItem> Items =>
            _itemResources.Cast<IItem>().
            Concat(_itemWeapons.Cast<IItem>().
                Concat(_itemEquipment.Cast<IItem>()));
    }
}
