using System;
using Inventarys.Data;
using ReactivePropertes;

namespace Inventorys.Slot
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        private readonly IReactiveProperty<int> _amount;
        private readonly IReactiveProperty<string> _itemId;

        public bool IsEmpty => _amount.Value <= 0;
        public IReadOnlyReactiveProperty<int> Amount => _amount;
        public IReadOnlyReactiveProperty<string> ItemId => _itemId;

        public InventorySlot()
        {
            _amount = new ReactiveProperty<int>();
            _itemId = new ReactiveProperty<string>();
        }

        public InventorySlot(InventorySlotData data)
        {
            _amount = new ReactiveProperty<int>(data.Amount);
            _itemId = new ReactiveProperty<string>(data.ItemId);
        }

        public void SetAmount(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"{nameof(value)} should not be zero");

            if (value == 0)
                _itemId.Value = string.Empty;

            _amount.Value = value;
        }

        public void SetItemId(string id, int value)
        {
            _itemId.Value = id;
            SetAmount(value);
        }
    }
}
