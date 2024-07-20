using System;

namespace Inventarys.Data
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId { get; set; }
        public int Amount { get; set; }

        public InventorySlotData() { }
        public InventorySlotData(string itemId, int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }
    }
}
