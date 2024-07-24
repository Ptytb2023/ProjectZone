using System;
using Inventorys.Slot;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Inventarys.View
{
    public class InventoryActionMenu : MonoBehaviour
    {
        [SerializeField] private PanelRemoveItem _panelRemoveItem;
        [SerializeField] private Button _buttonOpenPanelRemoveItem;

        [SerializeField] private Button _buttonDropItem;
        [SerializeField] private Button _buttonUseItem;

        private IReadOnlyInventorySlot _slot;

        private string ItemId => _slot.ItemId.GetValue();

        public event Action<IReadOnlyInventorySlot> ClickDropItem;
        public event Action<IReadOnlyInventorySlot> ClickUseItem;

        public event Action<IReadOnlyInventorySlot, int> RequestRemove;

        private void OnEnable() => 
            Subscribe();

        private void OnDisable() => 
            Unsubscribe();

        public void SetSlot(IReadOnlyInventorySlot slot,bool isUsabelItem)
        {
            gameObject.SetActive(true);

            if (_slot is not null)
                _slot.Amount.Unsubscribe(OnChageAmmoInSlot);

            _slot = slot;
            _slot.Amount.Subscribe(OnChageAmmoInSlot);

            if (!isUsabelItem)
            {
                _buttonUseItem.gameObject.SetActive(false);
                return;
            }

            _buttonUseItem.gameObject.SetActive(true);
        }

        private void OnClickOpenRemovePanel()
        {
            int maxRemove = _slot.Amount.GetValue();
            _panelRemoveItem.SetMaxRemoveAndOpenPanel(maxRemove);
        }

        private void OnClickDrop() =>
            ClickDropItem?.Invoke(_slot);

        private void OnClickUseItem() =>
            ClickUseItem?.Invoke(_slot);

        private void OnClickRemoveItem(int amount) => 
            RequestRemove?.Invoke(_slot, amount);

        private void Subscribe()
        {
            if (_slot is not null)
                _slot.Amount.Subscribe(OnChageAmmoInSlot);

            _panelRemoveItem.ClickRemove += OnClickRemoveItem;

            _buttonOpenPanelRemoveItem.onClick.AddListener(OnClickOpenRemovePanel);
            _buttonDropItem.onClick.AddListener(OnClickDrop);
            _buttonUseItem.onClick.AddListener(OnClickUseItem);
        }

        private void Unsubscribe()
        {
            _buttonOpenPanelRemoveItem.onClick.RemoveListener(OnClickOpenRemovePanel);
            _buttonDropItem.onClick.RemoveListener(OnClickDrop);
            _buttonUseItem.onClick.RemoveListener(OnClickUseItem);

            _panelRemoveItem.ClickRemove -= OnClickRemoveItem;

            _buttonUseItem.gameObject.SetActive(false);

            if (_slot is not null)
                _slot.Amount.Unsubscribe(OnChageAmmoInSlot);
        }

        public void OnChageAmmoInSlot(int value)
        {
            if (value > 0)
                return;

            gameObject.SetActive(false);
            _slot.Amount.Unsubscribe(OnChageAmmoInSlot);
        }
    }
}
