using System;

namespace Inventarys.Data
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId { get; set; }
        public int Amount { get; set; }
    }
}
