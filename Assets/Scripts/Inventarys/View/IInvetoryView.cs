using System;

namespace Inventarys
{
    public interface IInvetoryView
    {
        event Action<int> RequestDropItemInSlot;
        event Action<int, int> RequestRemoveItemInSlot;
        event Action<int> RequestUseItemInSlot;

        void CloseInventory();
        void OpenInventory();
    }
}