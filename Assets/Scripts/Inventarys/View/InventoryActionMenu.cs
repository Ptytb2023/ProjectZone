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

        private int _indexSlot;
        private string ItemId => _slot.ItemId.GetValue();

        public event Action<IReadOnlyInventorySlot> ClickDropItem;
        public event Action<IReadOnlyInventorySlot> ClickUseItem;

        public event Action<IReadOnlyInventorySlot, int> RequestRemove;

        private void OnEnable() => 
            Subscribe();

        private void OnDisable() => 
            UnsubscribeFromButtons();

        public void SetSlot(IReadOnlyInventorySlot slot,bool isUsabelItem)
        {
            _slot = slot;

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
            _panelRemoveItem.ClickRemove += OnClickRemoveItem;

            _buttonOpenPanelRemoveItem.onClick.AddListener(OnClickOpenRemovePanel);
            _buttonDropItem.onClick.AddListener(OnClickDrop);
            _buttonUseItem.onClick.AddListener(OnClickUseItem);
        }

        private void UnsubscribeFromButtons()
        {
            _buttonOpenPanelRemoveItem.onClick.RemoveListener(OnClickOpenRemovePanel);
            _buttonDropItem.onClick.RemoveListener(OnClickDrop);
            _buttonUseItem.onClick.RemoveListener(OnClickUseItem);

            _panelRemoveItem.ClickRemove -= OnClickRemoveItem;

            _buttonUseItem.gameObject.SetActive(false);
        }
    }
}
