namespace EquipmentInventores.Structures
{
    public struct ReplaceItemResult
    {
        public string ItemIdRemove { get; private set; }
        public string ItemIdAdded { get; private set; }
        public bool IsSuccess { get; private set; }

        public ReplaceItemResult(string itemIdRemove, string itemIdAdded, bool isSuccess)
        {
            ItemIdRemove = itemIdRemove;
            ItemIdAdded = itemIdAdded;
            IsSuccess = isSuccess;
        }
    }
}
