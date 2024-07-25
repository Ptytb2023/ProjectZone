using ItemSystem.Items.Equipments;
using UnityEngine;
using UnityEngine.UI;

namespace Player.EquipmentInventores
{
    [RequireComponent(typeof(Image))]
    public class EquipmentItemSlot : MonoBehaviour, IEquipmentItemSlot
    {
        [SerializeField] private EquipmentType _equipmentType;

        private Image _image;

        public EquipmentType Type => _equipmentType;

        private void Awake() =>
            _image = GetComponent<Image>();

        public void SetIcon(Sprite icon)
        {
            _image.enabled = true;
            _image.sprite = icon;
        }

        public void ResetIcon()
        {
            _image.enabled = false;
            _image.sprite = null;
        }
    }
}
