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

        private void OnDisable() => 
            UnsubscribeFromButtons();

        public void SetSlot(IReadOnlyInventorySlot slot,bool isUsabelItem)
        {
            _slot = slot;

            _buttonOpenPanelRemoveItem.onClick.AddListener(OnClickOpenRemovePanel);
            _buttonDropItem.onClick.AddListener(OnClickDrop);

            if (!isUsabelItem)
                return;

            _buttonUseItem.gameObject.SetActive(true);
            _buttonUseItem.onClick.AddListener(OnClickDrop);
        }

        private void OnClickOpenRemovePanel()
        {
            int maxRemove = _slot.Amount.GetValue();
            _panelRemoveItem.SetMaxRemoveAndOpenPanel(maxRemove);
            _panelRemoveItem.ClickRemove += OnClickRemoveItem;
        }

        private void OnClickDrop() =>
            ClickDropItem?.Invoke(_slot);

        private void OnClickUseItem() =>
            ClickUseItem?.Invoke(_slot);

        private void OnClickRemoveItem(int amount)
        {
            _panelRemoveItem.ClickRemove -= OnClickRemoveItem;
            RequestRemove?.Invoke(_slot, amount);
        }

        private void UnsubscribeFromButtons()
        {
            _buttonOpenPanelRemoveItem.onClick.RemoveListener(OnClickOpenRemovePanel);
            _buttonDropItem.onClick.RemoveListener(OnClickDrop);
            _buttonUseItem.onClick.RemoveListener(OnClickDrop);

            _panelRemoveItem.ClickRemove -= OnClickRemoveItem;

            _buttonUseItem.gameObject.SetActive(false);
        }
    }
}
