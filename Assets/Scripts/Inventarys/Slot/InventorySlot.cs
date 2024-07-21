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
            if (data.ItemId != string.Empty && data.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(data.Amount));

            _amount = new ReactiveProperty<int>(data.Amount);
            _itemId = new ReactiveProperty<string>(data.ItemId);
        }

        public void SetItemId(string id, int value)
        {
            SetItemId(id);
            SetAmount(value);
        }

        public void SetItemId(string id) =>
          _itemId.Value = id;

        public void SetAmount(int value)
        {
            EnsureItemIdIsNotNull();
            ValidateAmount(value);

            if (value == 0)
                _itemId.Value = string.Empty;

            _amount.Value = value;
        }


        private void ValidateAmount(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Amount should not be negative");
        }

        private void EnsureItemIdIsNotNull()
        {
            if (string.IsNullOrEmpty(_itemId.Value))
                throw new NullReferenceException("Cannot set amount if the object does not contain an itemID");
        }
    }
}
