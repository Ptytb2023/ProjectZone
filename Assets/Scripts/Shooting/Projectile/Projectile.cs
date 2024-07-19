using System.Collections;
using UnityEngine;

namespace Shooting.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private float _speedBullet;

        private Rigidbody2D _rigidbody;
        private float _damage;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        public void Shoot(Vector3 from, Vector3 direction, float damage)
        {
            transform.position = from;
            _damage = damage;

            StartCoroutine(StartShoot(direction));
        }

        private IEnumerator StartShoot(Vector3 direction)
        {
            while (enabled)
            {
                Vector2 newPosition = direction * _speedBullet * Time.deltaTime;
                Vector2 CurrentPostion = _rigidbody.position;

                _rigidbody.MovePosition(newPosition + CurrentPostion);

                yield return null;
            }
        }

    }
}
