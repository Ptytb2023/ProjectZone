using Inventorys.Slot;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventarys.View
{
    public class InventorySlotView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _quantityText;

        private int _index;
        private IReadOnlyInventorySlot _slot;

        private Color _startColor;
        private readonly Color _colorEmpty = new Color(0, 0, 0, 0);

        public Action<int> ClickSlot;

        private void Start() =>
            _startColor = _icon.color;

        private void OnDestroy() =>
            UnsubscribeFromSlotEvents();

        public void Init(IReadOnlyInventorySlot slot, int index)
        {
            if (slot == null)
                throw new ArgumentNullException(nameof(slot));

            _index = index;
            _slot = slot;

            SubscribeToSlotEvents();
        }

        private void SubscribeToSlotEvents()
        {
            _slot.ItemId.SubscribeAndUpdate(OnChangeIdItem);
            _slot.Amount.SubscribeAndUpdate(OnChangeAmount);
        }

        private void UnsubscribeFromSlotEvents()
        {
            if (_slot != null)
            {
                _slot.ItemId.Unsubscribe(OnChangeIdItem);
                _slot.Amount.Unsubscribe(OnChangeAmount);
            }
        }

        public void OnPointerDown(PointerEventData eventData) =>
            ClickSlot?.Invoke(_index);

        private void OnChangeIdItem(string idItem)
        {
            _icon.color = _startColor;
        }

        private void OnChangeAmount(int ammount)
        {
            if (ammount == 1)
            {
                _quantityText.text = string.Empty;
                return;
            }

            if (ammount <= 0)
            {
                ResetSlot();
                return;
            }

            _quantityText.text = ammount.ToString();
        }

        public void ResetSlot()
        {
            _quantityText.text = string.Empty;
            _icon.sprite = null;
            _icon.color = _colorEmpty;
        }
    }
}
