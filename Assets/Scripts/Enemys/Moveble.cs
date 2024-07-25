using Extensions;
using UnityEngine;

namespace Enemys
{
    public class Moveble
    {
        private float _moveSpeed;
        private Transform _transform;

        public Moveble(Transform transfrom,float moveSpeed)
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
