using System;
using UnityEngine;

namespace Inventarys.Data
{
    [Serializable]
    public class InventorySlotData
    {
        [field: SerializeField] public string ItemId { get; set; }
        [field: SerializeField] public int Amount { get; set; }

        public InventorySlotData() { }
        public InventorySlotData(string itemId, int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }
    }
}
