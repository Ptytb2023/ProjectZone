using System;
using System.Collections.Generic;

namespace Inventarys.Data
{
    [Serializable]
    public class InvetoryData
    {
        public int Size { get; set; }
        public List<InventorySlotData> Slots { get; set; }
        public SizeInvetoryData SizeInvetory { get; set; }

    }

}
