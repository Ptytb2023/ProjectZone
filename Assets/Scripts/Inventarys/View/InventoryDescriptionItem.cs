using Inventorys.Slot;
using ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventarys.View
{
    public class InventoryDescriptionItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameItemText;
        [SerializeField] private TMP_Text _descriptionText;

        private IReadOnlyInventorySlot _currentSlot;

        private void OnDisable() =>
            ResetParametrs();

        public void SetItem(IItem item)
        {
            _icon.sprite = item.Icon;
            _nameItemText.text = item.Name;
            _descriptionText.text = item.Description;
        }

        public void ResetParametrs()
        {
            _icon.sprite = null;
            _nameItemText.text = string.Empty;
            _descriptionText.text = string.Empty;
        }
    }
}
