using Inventarys.Model;
using Inventorys.Structures;
using System;

namespace Inventarys
{
    public interface IInventoryController : IExpandableInventory
    {
        event Action<string> DropItem;
        event Action<string> RequestUseItemInSlot;

        AddItemsResult AddItem(string idItem, int count = 1);
        RemoveItemResult RemoveItem(string idItem, int count = 1);
        RemoveItemResult RemoveAvailableItems(string idItem, int count = 1);
    }
}