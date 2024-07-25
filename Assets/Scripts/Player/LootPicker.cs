using Inventarys;
using Loots;
using PoolObject;
using Services;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class LootPicker : MonoBehaviour
    {
        private IPool<ItemLoot> _pool;
        private IItemService _itemService;
        private IInventoryController _inventoryController;

        [Inject]
        public void Construct(IPool<ItemLoot> pool,
                              IItemService itemService,
                              IInventoryController inventoryController)
        {
            _pool = pool;
            _itemService = itemService;
            _inventoryController = inventoryController;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ItemLoot item))
            {
                var result = _inventoryController.AddItem(item.Loot);

                if (result.AmountAdded < 0)
                    return;

                _pool.Return(item);
            }
        }
    }
}
