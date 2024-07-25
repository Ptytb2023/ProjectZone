using UnityEngine;

namespace Player
{
    public class PlayerOrientation : MonoBehaviour
    {
        [SerializeField] private PlayerMove _move;
        [SerializeField] private SpriteRenderer[] _spriteRenderers;

        private void Update()
        {
            if (_move.Direction.x > 0)
                Swap(false);
            else
                Swap(true);
        }

        private void Swap(bool isSwap)
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
                _spriteRenderers[i].flipX = isSwap;
        }

    }
}
