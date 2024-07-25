using Enemys;
using PoolObject;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Shooting.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile, IPoolabel
    {
        [SerializeField] private float _speedBullet;

        private IPool<Projectile> _pool;
        private Rigidbody2D _rigidbody;
        private float _damage;

        [Inject]
        public void Construct(IPool<Projectile> pool) =>
            _pool = pool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        public void Shoot(Vector2 from, Vector2 direction, float damage)
        {
            if (direction == Vector2.zero)
                throw new ArgumentOutOfRangeException($"{nameof(direction)} cannot be {Vector2.zero}");

            transform.position = from;
            _damage = damage;

            StartCoroutine(StartShoot(direction));
        }

        private IEnumerator StartShoot(Vector2 direction)
        {
            while (enabled)
            {
                Vector2 CurrentPostion = _rigidbody.position;
                Vector2 newPosition = direction * _speedBullet * Time.fixedDeltaTime;

                _rigidbody.MovePosition(newPosition + CurrentPostion);

                yield return new WaitForFixedUpdate();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out Enemy enemy))
                return;

            enemy.TakeDemage(_damage);
            _pool.Return(this);
        }

        public void OnSpawn()
        {
        }

        public void OnDisapw()
        {
        }
    }
}
