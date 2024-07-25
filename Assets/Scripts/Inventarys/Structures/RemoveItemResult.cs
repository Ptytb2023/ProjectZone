namespace Inventorys.Structures
{
    public struct RemoveItemResult
    {
        public readonly string ItemId;
        public readonly int AmountRemoved;
        public readonly bool Success;

        public RemoveItemResult(string itemId, int amountRemoved, bool success)
        {
            ItemId = itemId;
            AmountRemoved = amountRemoved;
            Success = success;
        }
    }
}
