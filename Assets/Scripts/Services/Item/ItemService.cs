using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using Unity.Mathematics;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly Dictionary<string, IItem> _items;

        public ItemService(IEnumerable<IItem> items)
        {
            _items = new Dictionary<string, IItem>();

            CreateItems(items);
        }

        public IItem GetItem(string itemId)
        {
            if (!_items.TryGetValue(itemId, out var item))
                throw new KeyNotFoundException($"The item with the ID {itemId}'" +
                    $" was not found in the collection.");

            return item;
        }

        public T GetItem<T>(string itemId) where T : IItem
        {
            var item = GetItem(itemId);

            return (T)item;
        }

        public bool ContainsItem(string itemId) =>
            _items.ContainsKey(itemId);

        public IItem GetRandomItem()
        {
            int randomIndex = Random.Range(0,_items.Count);
            return _items.ElementAt(randomIndex).Value;
        }

        private void CreateItems(IEnumerable<IItem> items)
        {
            foreach (var item in items)
            {
                if (item is null || string.IsNullOrEmpty(item.Id))
                    throw new ArgumentNullException(nameof(item), "Item cannot be null.");

                _items.Add(item.Id, item);
            }
        }
    }
}
