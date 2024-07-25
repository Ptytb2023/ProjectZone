using Inventarys;
using ItemSystem;
using ItemSystem.Item;
using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Model;
using Services;
using Shooting;
using UnityEngine;
using Zenject;

namespace Player.EquipmentInventores
{
    public class InventoryEquipmentController : MonoBehaviour
    {
        [SerializeField] private CharacterAppearanceController _apperanceController;
        [SerializeField] private WeaponSystem _weaponSystem;

        private IItemService _itemService;
        private IInventoryEquipmentView _inventoryView;
        private IInventoryEquipment _inventoryEquipment;
        private IInventoryController _inventoryController;

        [Inject]
        private void Construct(IInventoryEquipment inventoryEquipment,
                               IInventoryEquipmentView inventoryEquipmentView,
                               IItemService itemService,
                               IInventoryController inventoryController)
        {
            _inventoryController = inventoryController;
            _itemService = itemService;
            _inventoryView = inventoryEquipmentView;
            _inventoryEquipment = inventoryEquipment;
        }

        private void Start() =>
            Initilize();

        private void OnEnable() =>
            _inventoryController.RequestUseItemInSlot += OnRequestUseItemInSlot;

        private void OnDisable() =>
            _inventoryController.RequestUseItemInSlot -= OnRequestUseItemInSlot;

        private void Initilize()
        {
            foreach (var slot in _inventoryEquipment.EquipmentsSlots)
            {
                if (slot.isEmpty)
                    continue;

                ItemEquipment item = _itemService.GetItem<ItemEquipment>(slot.ItemID);
                UpdateView(item);

                if (slot.Type == EquipmentType.Weapon)
                    EquipWeapon((ItemWeapon)item);
            }
        }

        private void OnRequestUseItemInSlot(string itemId)
        {
            var item = _itemService.GetItem(itemId);

            if (!(item.Type == ItemType.Equipment))
                return;

            var itemEquipment = _itemService.GetItem<ItemEquipment>(itemId);

            ReplaceEquipment(itemEquipment);
        }

        private void ReplaceEquipment(ItemEquipment item)
        {
            var result = _inventoryEquipment.ReplaceItem(item.EquipmentType, item.Id);

            if (!result.IsSuccess)
                return;

            UpdateView(item);
            SwapItems(item, result.ItemIdRemove);

            if (item.EquipmentType == EquipmentType.Weapon)
                EquipWeapon((ItemWeapon)item);
        }

        private void UpdateView(ItemEquipment itemEquipment)
        {
            _inventoryView.SetIcon(itemEquipment);
            _apperanceController.SetIcon(itemEquipment);
        }

        private void SwapItems(ItemEquipment newItem, string removedItemId)
        {
            _inventoryController.RemoveItem(newItem);

            if (string.IsNullOrEmpty(removedItemId))
                return;

            var itemRemoved = _itemService.GetItem(removedItemId);
            _inventoryController.AddItem(newItem);
        }

        private void EquipWeapon(ItemWeapon weapon) =>
            _weaponSystem.EquipWeapon(weapon.Weapon);
    }
}
