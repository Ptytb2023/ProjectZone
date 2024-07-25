using System;
using Inventorys.Slot;
using Services;
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

        private IItemService _itemService;

        public int Index { get; private set; }
        public IReadOnlyInventorySlot InveltorySlot { get; private set; }

        public event Action<InventorySlotView> ClickSlot;


        private void OnDestroy() =>
            UnsubscribeFromSlotEvents();

        public void Init(IItemService itemService) =>
            _itemService = itemService;

        public void SetSlot(IReadOnlyInventorySlot slot, int index)
        {
            if (slot is null)
                throw new ArgumentNullException(nameof(slot));

            Index = index;
            InveltorySlot = slot;

            SubscribeToSlotEvents();
        }

        public void OnPointerDown(PointerEventData eventData) =>
            ClickSlot?.Invoke(this);

        public void ResetSlot()
        {
            _quantityText.text = string.Empty;
            _icon.sprite = null;
            _icon.enabled = false;
        }

        private void OnChangeIdItem(string idItem)
        {
            if (string.IsNullOrEmpty(idItem))
            {
                ResetSlot();
                return;
            }

            _icon.enabled = true;
            var item = _itemService.GetItem(idItem);
            UpdateSlot(item.Icon);
        }

        private void UpdateSlot(Sprite icon) =>
            _icon.sprite = icon;

        private void OnChangeAmount(int ammount)
        {
            if (ammount == 1)
            {
                _quantityText.text = string.Empty;
                return;
            }
            else if (ammount <= 0)
            {
                ResetSlot();
                return;
            }

            _quantityText.text = ammount.ToString();
        }

       

        private void SubscribeToSlotEvents()
        {
            InveltorySlot.ItemId.SubscribeAndUpdate(OnChangeIdItem);
            InveltorySlot.Amount.SubscribeAndUpdate(OnChangeAmount);
        }

        private void UnsubscribeFromSlotEvents()
        {
            if (InveltorySlot is null)
                return;

            InveltorySlot.ItemId.Unsubscribe(OnChangeIdItem);
            InveltorySlot.Amount.Unsubscribe(OnChangeAmount);
        }
    }
}
