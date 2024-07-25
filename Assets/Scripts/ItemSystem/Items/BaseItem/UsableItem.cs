using UnityEngine;

namespace ItemSystem.Items
{
    public abstract class UsableItem : BaseItem
    {
        public override ItemType Type => GetItemType();
        public override bool IsUsable => true;
        

        protected abstract ItemType GetItemType();
    }
}
