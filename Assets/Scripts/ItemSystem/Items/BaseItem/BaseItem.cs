using System;
using UnityEngine;

namespace ItemSystem.Items
{
    [Serializable]
    public abstract class BaseItem : IItem
    {
        [field: SerializeField] public string Id { get; private set; }


        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }

        [field: TextArea]
        [field: SerializeField] public string Description { get; private set; }


        [field: Min(1)]
        [field: SerializeField] public int MaxStack { get; private set; } = 1;

        public bool IsStackable => MaxStack > 1;

        public abstract ItemType Type { get; }
        public abstract bool IsUsable { get; }
    }
}
