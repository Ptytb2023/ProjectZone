using Inventarys;
using ItemSystem.Item;
using ItemSystem.Items.Equipments;
using Lean.Pool;
using Player.EquipmentInventores.Model;
using Shooting;
using UnityEngine;
using Zenject;

namespace Player.EquipmentInventores
{
    public class InventoryEquipmentController : MonoBehaviour, IInventoryEquipmentController
    {
        [SerializeField] private CharacterAppearanceController _apperanceController;
        [SerializeField] private WeaponSystem _weaponSystem;
        [SerializeField] private InventoryController _inventoryController;

        private ItemWeapon _currentWeaponItem;
        private IInventoryEquipmentView _inventoryView;
        private IInventoryEquipment _inventoryEquipment;

        [Inject]
        private void Construct(IInventoryEquipment inventoryEquipment) =>
            _inventoryEquipment = inventoryEquipment;

        private void Awake()
        {
            _inventoryEquipment.Initilize(_inventoryController);
            _inventoryView.Init(_inventoryEquipment);
            _apperanceController.Init(_inventoryEquipment);
        }

        private void OnEnable() => 
            _inventoryEquipment.ChangedEquipment += OnChangedEquipment;

        private void OnDisable() => 
            _inventoryEquipment.ChangedEquipment -= OnChangedEquipment;

        public bool TrySetEquipment(ItemEquipment equipment) => 
            _inventoryEquipment.TryСlothe(equipment);


        public void OnChangedEquipment(EquipmentType type, ItemEquipment item)
        {
            if (!(type == EquipmentType.Weapon))
                return;

            if (_currentWeaponItem is not null)
                LeanPool.Despawn(_currentWeaponItem.Weapon);

            var weaponObject = LeanPool.Spawn(_currentWeaponItem.Weapon);
            weaponObject.transform.SetParent(_apperanceController.WeaponPoint);

            _weaponSystem.EquipWeapon(weaponObject);
        }
    }
}
