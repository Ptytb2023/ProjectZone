using Inventarys.Data;

namespace ItemSystem
{
    public interface IItem : IInventoryItem
    {
        string Name { get; }
        string Description { get; }
        string Type { get; }

        bool IsUsable { get; }
        void UseItem();
    }
}
