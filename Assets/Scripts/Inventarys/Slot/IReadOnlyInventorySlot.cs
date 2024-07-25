using ReactivePropertes;

namespace Inventorys.Slot
{
    public interface IReadOnlyInventorySlot
    {
        public bool IsEmpty { get; }
        public IReadOnlyReactiveProperty<int> Amount { get; }
        public IReadOnlyReactiveProperty<string> ItemId { get; }
    }

}
