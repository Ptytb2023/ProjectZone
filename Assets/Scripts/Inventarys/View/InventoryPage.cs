using Inventorys.Slot;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventarys.View
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private InventorySlotView _prefabInventorySlot;
        [SerializeField] private RectTransform _contentPanel;

        private List<InventorySlotView> _slots;

        public event Action<IReadOnlyInventorySlot, int> ClickSlot;


        private void OnEnable()
        {
            if (_slots is null)
                return;

            foreach (var slot in _slots)
                slot.ClickSlot += OnClickSlot;
        }

        private void OnDisable()
        {
            foreach (var slot in _slots)
                slot.ClickSlot -= OnClickSlot;
        }

        public void Init(IReadOnlyInventorySlot[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlotView slotView = Instantiate(_prefabInventorySlot, _contentPanel);

                slotView.SetSlot(slots[i], i);
                slotView.ClickSlot += OnClickSlot;
                _slots.Add(slotView);
            }
        }

        private void OnClickSlot(IReadOnlyInventorySlot slot, int index) =>
            ClickSlot?.Invoke(slot, index);
    }
}
