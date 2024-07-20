﻿using System;

namespace Inventorys.Structures
{
    public struct AddItemsResult
    {
        public readonly string ItemId;
        public readonly int AmountToAdd;
        public readonly int AmountAdded;

        public int Remains => AmountToAdd - AmountAdded;

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
    }
}
