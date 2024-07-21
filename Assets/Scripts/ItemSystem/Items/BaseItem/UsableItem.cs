using UnityEngine;

namespace ItemSystem.Items
{
    public abstract class UsableItem : BaseItem, IUsabelItem
    {
        public override ItemType Type => GetItemType();
        public override bool IsUsable => true;
        

        public abstract bool TryUseItem(GameObject target);
        protected abstract ItemType GetItemType();
    }
}
