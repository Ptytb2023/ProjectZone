using Inventarys;
using Loots;
using UnityEngine;
using Zenject;

namespace Player
{
    public class DropLissenerInventory : MonoBehaviour
    {
        private IInventoryController _inventoryController;
        private IItemDropper _itemDropper;

        [Inject]
        public void Construct(IInventoryController inventoryController, IItemDropper itemDrpper)
        {
            _inventoryController = inventoryController;
            _itemDropper = itemDrpper;
        }

        private void OnEnable() =>
            _inventoryController.DropItem += OnDropItem;

        private void OnDisable() =>
            _inventoryController.DropItem -= OnDropItem;

        private void OnDropItem(string itemId) => 
            _itemDropper.DropItemForward(transform, itemId);
    }
}
