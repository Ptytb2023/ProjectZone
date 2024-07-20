using System;
using System.Collections.Generic;

namespace Inventarys.Data
{
    [Serializable]
    public class InventoryData
    {
        public int Size { get; set; }
        public List<InventorySlotData> Slots { get; set; }

    }

}
