using System;

namespace ItemSystem.Items
{
    [Serializable]
    public class ItemResource : BaseItem
    {
        public override ItemType Type => ItemType.None;

        public override bool IsUsable => false;
    }
}
