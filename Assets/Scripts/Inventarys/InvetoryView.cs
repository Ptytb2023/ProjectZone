using Inventarys.View;
using Inventorys.Slot;
using ItemSystem;
using Services;
using Services.Input;
using System;
using UnityEngine;
using Zenject;

namespace Inventarys
{
    public class InvetoryView : MonoBehaviour
    {
        [SerializeField] private InventoryPage _inventoryPage;
        [SerializeField] private InventoryDescriptionItem _inventoryDescription;
        [SerializeField] private InventoryActionMenu _inventoryActionMenu;

        private int _currentIndexSelectedSlot;
        private IItemService _itemService;

        public event Action<int> RequestDropItemInSlot;
        public event Action<int> RequestUseItemInSlot;
        public event Action<int, int> RequestRemoveItemInSlot;

        [Inject]
        private void Construct(IItemService itemService) =>
            _itemService = itemService;

        private void OnEnable()
        {
            _inventoryPage.ClickSlot += OnClickInventorySlot;

            _inventoryActionMenu.ClickDropItem += OnClickDrop;
            _inventoryActionMenu.ClickUseItem += OnClickUseItem;
            _inventoryActionMenu.RequestRemove += OnRequestRemove;
        }

        private void OnDisable()
        {
            _inventoryPage.ClickSlot -= OnClickInventorySlot;

            _inventoryActionMenu.ClickDropItem -= OnClickDrop;
            _inventoryActionMenu.ClickUseItem -= OnClickUseItem;
            _inventoryActionMenu.RequestRemove -= OnRequestRemove;

            _inventoryActionMenu.gameObject.SetActive(false);
        }

        public void Init(IReadOnlyInventorySlot[] slots) =>
            _inventoryPage.Init(slots);

        public void OpenInventory() => 
            gameObject.SetActive(true);

        public void CloseInventory() => 
            gameObject.SetActive(false);

        private void OnClickInventorySlot(IReadOnlyInventorySlot slot, int index)
        {
            _currentIndexSelectedSlot = index;

            IItem item = _itemService.GetItem(slot.ItemId.GetValue());

            _inventoryDescription.SetItem(item);
            _inventoryActionMenu.SetSlot(slot, item.IsUsable);
            _inventoryActionMenu.gameObject.SetActive(true);
        }

        private void OnClickDrop(IReadOnlyInventorySlot slot) => 
            RequestDropItemInSlot?.Invoke(_currentIndexSelectedSlot);

        private void OnClickUseItem(IReadOnlyInventorySlot slot) => 
            RequestUseItemInSlot?.Invoke(_currentIndexSelectedSlot);

        private void OnRequestRemove(IReadOnlyInventorySlot slot, int amount) => 
            RequestRemoveItemInSlot?.Invoke(_currentIndexSelectedSlot, amount);

    }
}
