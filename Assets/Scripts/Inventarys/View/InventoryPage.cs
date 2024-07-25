using Inventorys.Slot;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Inventarys.View
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private GameObject _cursor;
        [SerializeField] private InventorySlotView _prefabInventorySlot;
        [SerializeField] private RectTransform _contentPanel;

        private List<InventorySlotView> _slots = new List<InventorySlotView>();

        public event Action<IReadOnlyInventorySlot, int> ClickSlot;


        private IItemService _itemService;

        [Inject]
        private void Construct(IItemService itemService) =>
            _itemService = itemService;


        private void OnEnable()
        {
            if (_slots is null)
                return;

            foreach (var slot in _slots)
                slot.ClickSlot += OnClickSlot;
        }

        private void OnDisable()
        {
            if (_slots is not null)
                foreach (var slot in _slots)
                    slot.ClickSlot -= OnClickSlot;

            _cursor.gameObject.SetActive(false);
        }

        public void Init(IReadOnlyInventorySlot[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlotView slotView = Instantiate(_prefabInventorySlot, _contentPanel);

                slotView.Init(_itemService);
                slotView.SetSlot(slots[i], i);
                slotView.ClickSlot += OnClickSlot;
                _slots.Add(slotView);
            }
        }

        private void OnClickSlot(InventorySlotView view)
        {
            _cursor.gameObject.SetActive(true);
            _cursor.transform.parent = view.transform;
            _cursor.transform.position = view.transform.position;

            ClickSlot?.Invoke(view.InveltorySlot, view.Index);
        }
    }
}
