using System;
using System.Collections.Generic;

namespace Inventarys.Data
{
    [Serializable]
    public class InvetoryData
    {
        public int Size { get; set; }
        public List<InventorySlotData> Slots { get; set; }

        public InvetoryData() { }

        public InvetoryData(int size, List<InventorySlotData> slots)
        {
            Size = size;
            Slots = slots;
        }

    }

}
