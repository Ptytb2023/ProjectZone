namespace Inventarys.Data
{
    public interface IInventoryItem
    {
        string Id { get; }
        bool IsStackable { get; }
        int MaxStack { get; }
    }
}