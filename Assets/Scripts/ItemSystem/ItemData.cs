using ItemSystem.Item;
using ItemSystem.Items;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "ItemData", fileName = "ScriptableObject/ItemSystem/ItemData", order = 51)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private List<ItemResource> _itemResources;
        [SerializeField] public List<ItemWeapon> _itemWeapons;

        public IEnumerable<IItem> Items => _itemResources.Cast<IItem>().Concat(_itemWeapons.Cast<IItem>());
    }
}
