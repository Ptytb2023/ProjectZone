using ItemSystem;

namespace Inventorys.Item
{
    public interface IInvetoryItem
    {
        public string ID { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsStackable { get; }
        public ItemType Type { get; }
    }
}