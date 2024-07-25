using Extensions;
using PoolObject;
using Services;
using UnityEngine;

namespace Loots
{
    public class ItemDropper : IItemDropper
    {
        private const float AngleDrop = 360;

        private const float MinAngleDropForward = -30;
        private const float MaxAngleDropForward = 30;

        private IItemService _itemService;
        private IPool<ItemLoot> _pool;

        public ItemDropper(IItemService itemService, IPool<ItemLoot> pool)
        {
            _itemService = itemService;
            _pool = pool;
        }

        public void DropItem(Transform dropper, string itemId)
        {
            ItemLoot loot = GetItem(itemId);

            Vector2 direction = dropper.GetVector2DirectionForwardRange(0, AngleDrop);
            loot.Drop(dropper.position, direction);
        }

        public void DropItemForward(Transform dropper, string itemId)
        {
            ItemLoot loot = GetItem(itemId);

            Vector2 direction = dropper.GetVector2DirectionForwardRange(MinAngleDropForward, MaxAngleDropForward);
            loot.Drop(dropper.position, direction);
        }

        private ItemLoot GetItem(string itemId)
        {
            var loot = _pool.Request();
            var item = _itemService.GetItem(itemId);

            loot.AssignItem(item);
            return loot;
        }
    }
}
