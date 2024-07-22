using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.EquipmentInventores
{
    public class InventoryEquipmentView : MonoBehaviour, IInventoryEquipmentView
    {
        [SerializeField] private List<EquipmentItemSlot> _itemIcons;

        private Dictionary<EquipmentType, EquipmentItemSlot> _icons;
        private IInventoryEquipment _inventoryEquipment;

        private void Awake()
        {
            _icons = new Dictionary<EquipmentType, EquipmentItemSlot>();

            foreach (var icon in _itemIcons)
                _icons.Add(icon.Type, icon);
        }

        private void OnDestroy()
        {
            if (_inventoryEquipment is null)
                return;

            _inventoryEquipment.ChangedEquipment -= OnChangedEquipment;
        }

        public void Init(IInventoryEquipment inventoryEquipment)
        {
            if (_inventoryEquipment is not null)
                throw new InvalidOperationException("Inventory equipment is already set.");

            _inventoryEquipment = inventoryEquipment;
            InitializeItemIcons();

            _inventoryEquipment.ChangedEquipment += OnChangedEquipment;
        }

        public void SetIcon(EquipmentType type, Sprite icon) =>
            _icons[type].SetIcon(icon);

        public void ResetAllIcon()
        {
            if (_icons is null)
                return;

            foreach (var item in _icons.Values)
                item.ResetIcon();
        }
      

        private void OnChangedEquipment(EquipmentType type, ItemEquipment item) =>
            SetIcon(type, item.Icon);

        private void InitializeItemIcons()
        {
            foreach (var item in _inventoryEquipment.ItemEquipments)
                SetIcon(item.EquipmentType, item.Icon);
        }
    }
}
