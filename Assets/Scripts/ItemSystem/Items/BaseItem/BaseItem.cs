using System;
using UnityEngine;

namespace ItemSystem.Items
{
    [Serializable]
    public abstract class BaseItem : IItem
    {
        [field: SerializeField] public string Id { get; }


        [field: Header("Item Information")]
        [field: SerializeField] public Sprite Icon { get; }
        [field: SerializeField] public string Name { get; }

        [field: TextArea]
        [field: SerializeField] public string Description { get; }


        [field: Header("Storage in the inventory")]

        [field: Min(1)]
        [field: SerializeField] public int MaxStack { get; }

        public bool IsStackable => MaxStack > 1;

        public abstract ItemType Type { get; }
        public abstract bool IsUsable { get; }
    }
}
