using Inventarys.Data;
using Inventarys.Model;
using Inventorys.Structures;
using Services;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Inventarys
{
    public class InventoryController : MonoBehaviour, IItemAdder
    {
        private IItemService _itemService;
        private IInputService _inputService;

        private IInvetoryView _invetoryView;
        private IInventory _inventory;

        [Inject]
        private void Construct(IInventory data, IInvetoryView view, IInputService inputService)
        {
            _inventory = data;
            _invetoryView = view;
            _inputService = inputService;
        }

        private void Start() =>
            Initialization();

        private void OnEnable() =>
            Subscriptions();

        private void OnDisable() =>
        Unsubscribes();

        public AddItemsResult AddItem(int slotIndex, IInventoryItem item, int count) =>
            _inventory.AddItem(slotIndex, item, count);

        public AddItemsResult AddItem(IInventoryItem item, int count) =>
            _inventory.AddItem((IInventoryItem)item, count);


        private void OnPressedOpenInventory() =>
            _invetoryView.OpenInventory();

        private void OnRequestRemoveItemInSlot(int indexSlot, int amount) =>
            _inventory.RemoveItem(indexSlot, amount);

        private void OnRequestDropItemInSlot(int indexSlot)
        {
            _inventory.RemoveItem(indexSlot, 1);
        }

        private void OnRequestUseItemInSlot(int indexSlot)
        {
            var resultRemove = _inventory.RemoveItem(indexSlot, 1);

            if (!resultRemove.Success)
                return;

            _itemService.TryUseItem(resultRemove.ItemId, gameObject);
        }

        private void Initialization()
        {
            var slots = _inventory.InventorySlots;
            _invetoryView.Init(slots);
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

    }
}
