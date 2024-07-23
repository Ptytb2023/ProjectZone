using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventarys.Data
{
    [Serializable]
    public class InventoryData
    {
        [field:SerializeField] public int Size { get; set; }
        [field:SerializeField] public List<InventorySlotData> Slots { get; set; }

        public InventoryData() { }

        public InventoryData(int size, List<InventorySlotData> slots)
        {
            Size = size;
            Slots = slots;
        }

    }

}
