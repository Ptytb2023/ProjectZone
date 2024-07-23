using ItemSystem.Items.Equipments;
using Player.EquipmentInventores;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterEquipmentSlot : MonoBehaviour, IEquipmentItemSlot
    {
        [SerializeField] private EquipmentType _eqipmenType;

        private Sprite _startSprite;
        private SpriteRenderer _spriteRemdere;

        public EquipmentType Type => _eqipmenType;

        private void Awake()
        {
            _spriteRemdere = GetComponent<SpriteRenderer>();
            _startSprite = _spriteRemdere.sprite;
        }

        public void SetIcon(Sprite icon) => 
            _spriteRemdere.sprite = icon;

        public void ResetIcon() => 
            _spriteRemdere.sprite = _startSprite;
    }
}
