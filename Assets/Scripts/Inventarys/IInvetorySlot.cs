using Inventorys.Item;

namespace Inventorys
{
    public interface IInvetorySlot
    {
        public int Capacity { get; }
        public int Cout { get; }
        public IInvetoryItem Item { get; }
    }
}
