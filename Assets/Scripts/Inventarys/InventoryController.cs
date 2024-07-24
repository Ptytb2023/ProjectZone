using Inventarys.Data;
using Inventarys.Model;
using Inventorys.Structures;
using Services;
using Services.Input;
using System;
using UnityEngine;
using Zenject;

namespace Inventarys
{
    public class InventoryController : IInventoryController
    {
        private IItemService _itemService;
        private IInputService _inputService;

        private IInvetoryView _invetoryView;
        private IInventory _inventory;

        public event Action<string> DropItem;
        public event Action<string> RequestUseItemInSlot;

        [Inject]
        public InventoryController(IItemService itemService,
                                   IInputService inputService,
                                   IInvetoryView invetoryView,
                                   IInventory inventory)
        {
            _itemService = itemService;
            _inputService = inputService;
            _invetoryView = invetoryView;
            _inventory = inventory;

            Initialization();
            Subscriptions();
        }

        public AddItemsResult AddItem(string idItem, int count = 1)
        {
            if (!(_itemService.ContainsItem(idItem)))
                return new AddItemsResult(idItem, count, 0);

            var item = _itemService.GetItem(idItem);
            return AddItem(item, count);
        }

        public AddItemsResult AddItem(int slotIndex, IInventoryItem item, int count) =>
            _inventory.AddItem(slotIndex, item, count);

        public AddItemsResult AddItem(IInventoryItem item, int count = 1) =>
            _inventory.AddItem(item, count);

        public RemoveItemResult RemoveItem(int slotIndex, int count = 1) =>
            _inventory.RemoveItem(slotIndex, count);

        public RemoveItemResult RemoveItem(IInventoryItem item, int count = 1) =>
            _inventory.RemoveItem(item, count);

        public RemoveItemResult RemoveItem(string idItem, int count = 1)
        {
            if (!(_itemService.ContainsItem(idItem)))
                return new RemoveItemResult(idItem, 0, false);

            var item = _itemService.GetItem(idItem);

            return RemoveItem(item, count);
        }

        public RemoveItemResult RemoveAvailableItems(string idItem, int count = 1)
        {
            int availdCount = _inventory.GetItemAmount(idItem);

            if (availdCount <= 0)
                return new RemoveItemResult(idItem, count, false);

            int remove = Mathf.Clamp(availdCount, count, availdCount);
            return RemoveItem(idItem, remove);
        }

        private void Initialization()
        {
            var slots = _inventory.InventorySlots;
            _invetoryView.Init(slots);
        }

        private void OnPressedOpenInventory() =>
            _invetoryView.OpenInventory();

        private void OnRequestRemoveItemInSlot(int indexSlot, int amount) =>
            _inventory.RemoveItem(indexSlot, amount);

        private void OnRequestDropItemInSlot(int indexSlot)
        {
            var itemId = _inventory.GetItemIdInSlot(indexSlot);
            _inventory.RemoveItem(indexSlot);

            DropItem?.Invoke(itemId);
        }

        private void OnRequestUseItemInSlot(int indexSlot)
        {
            var itemId = _inventory.GetItemIdInSlot(indexSlot);
            RequestUseItemInSlot?.Invoke(itemId);
        }

        private void Subscriptions()
        {
            _inputService.PressedOpenInventory += OnPressedOpenInventory;

            _invetoryView.RequestRemoveItemInSlot += OnRequestRemoveItemInSlot;
            _invetoryView.RequestDropItemInSlot += OnRequestDropItemInSlot;
            _invetoryView.RequestUseItemInSlot += OnRequestUseItemInSlot;
        }

        private void Unsubscribes()
        {
            _inputService.PressedOpenInventory -= OnPressedOpenInventory;

            _invetoryView.RequestRemoveItemInSlot -= OnRequestRemoveItemInSlot;
            _invetoryView.RequestDropItemInSlot -= OnRequestDropItemInSlot;
            _invetoryView.RequestUseItemInSlot -= OnRequestUseItemInSlot;
        }

        public void Dispose() => 
            Unsubscribes();
    }
}
