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

        public event Action<int> ClickSlot;

        public void Init(InventorySlot[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlotView slotView = Instantiate(_prefabInventorySlot, _contentPanel);

                slotView.SetSlot(slots[i], i);
                slotView.ClickSlot += ClickSlot;
                _slots.Add(slotView);
            }
        }

        private void OnDestroy()
        {
            foreach (var slot in _slots)
                slot.ClickSlot -= ClickSlot;
        }
    }
}
