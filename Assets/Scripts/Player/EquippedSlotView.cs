using ItemSystem.Items.Equipments;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EquippedSlotView : MonoBehaviour
    {
        [SerializeField] private EquipmentType _eqipmenType;

        private SpriteRenderer _spriteRemdere;

        public EquipmentType EquipmentType => _eqipmenType;

        private void Awake() => 
            _spriteRemdere = GetComponent<SpriteRenderer>();

        public void SetItemEqupment(Sprite icon) => 
            _spriteRemdere.sprite = icon;
    }
}
