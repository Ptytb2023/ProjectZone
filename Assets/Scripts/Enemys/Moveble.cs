using Extensions;
using UnityEngine;

namespace Enemys
{
    public class Moveble
    {
        private const int RotationByBack = 180;

        private float _moveSpeed;
        private Transform _transform;

        public Moveble(Transform transfrom, float moveSpeed)
        {
            _moveSpeed = moveSpeed;
            _transform = transfrom;
        }

        public void Move(Transform to)
        {
            Vector2 curentPostion = _transform.position;
            Vector2 direction = _transform.GetVector2Direction(to);

            Vector2 newPostion = curentPostion + direction * _moveSpeed * Time.deltaTime;
            _transform.position = newPostion;

            Rotate(direction);
        }

        private void Rotate(Vector2 direction)
        {
            Vector3 newLocalScale = Vector3.one;

            if (direction.x < 0)
                newLocalScale.x = -1;
            else
                newLocalScale.x = 1;

            _transform.localScale = newLocalScale;
        }
    }

}
