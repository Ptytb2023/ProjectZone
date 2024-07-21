using System;
using System.Collections.Generic;
using ItemSystem;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly Dictionary<string, IItem> _items;

        public ItemService(IEnumerable<IItem> items)
        {
            _items = new Dictionary<string, IItem>();


            foreach (var item in items)
            {
                if (item is null || string.IsNullOrEmpty(item.Id))
                    throw new ArgumentNullException(nameof(item), "Item cannot be null.");

                _items.Add(item.Id, item);
            }
        }

        public IItem GetItem(string itemId)
        {
            if (!_items.TryGetValue(itemId, out var item))
                throw new KeyNotFoundException($"The item with the ID {itemId}'" +
                    $" was not found in the collection.");

            return item;
        }

        public bool ContainsItem(string itemId) =>
            _items.ContainsKey(itemId);
    }
}
