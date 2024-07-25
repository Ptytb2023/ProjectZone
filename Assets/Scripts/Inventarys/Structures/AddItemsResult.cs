using System;

namespace Inventorys.Structures
{
    public struct AddItemsResult
    {
        public readonly string ItemId;
        public readonly int AmountToAdd;
        public readonly int AmountAdded;

        public int Remains => AmountAdded > 0 ? AmountToAdd - AmountAdded : 0;

        public AddItemsResult(string itemId, int amountToAdd, int amountAdded)
        {
            if (amountAdded > amountToAdd)
                throw new
                    ArgumentOutOfRangeException($"" +
                    $"{nameof(amountAdded)} " +
                    $"must not be greater than " +
                    $"{nameof(amountToAdd)}");

            ItemId = itemId;
            AmountToAdd = amountToAdd;
            AmountAdded = amountAdded;
        }

        public override string ToString() =>
            $"ID:{ItemId}, AmountToAdd: {AmountToAdd}, AmountAdded: {AmountAdded}";
    }
}
