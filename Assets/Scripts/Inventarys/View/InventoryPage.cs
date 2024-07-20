using Inventorys.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace Inventarys.View
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private InventorySlotView _prefabInventorySlot;

        [SerializeField] private RectTransform _contentPanel;


        private List<InventorySlotView> _slots;


        public void Init(InventorySlot[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                var slotView = Instantiate(_prefabInventorySlot, _contentPanel);

                slotView.Init(slots[i], i);
                _slots.Add(slotView);
            }
        }

    }
}
