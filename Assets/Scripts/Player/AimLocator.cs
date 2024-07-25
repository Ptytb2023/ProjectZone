using Enemys;
using Extensions;
using Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(TriggerObserver))]
    public class AimLocator : MonoBehaviour
    {
        [SerializeField] private WeaponSystem _weaponPoint;

        private List<Enemy> _enemys = new List<Enemy>();

        private TriggerObserver _triggerObserver;
        private Enemy _currentTarget;
        private Coroutine _aimProcess;

        private void Awake() => 
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable()
        {
            _triggerObserver.TrigerEnter += OnTriggerEnterObserver;
            _triggerObserver.TrigerExit += OnTriggerExitObserver;
        }

        private void OnDisable()
        {
            _triggerObserver.TrigerEnter -= OnTriggerEnterObserver;
            _triggerObserver.TrigerExit -= OnTriggerExitObserver;

            for (int i = 0; i < _enemys.Count; i++)
                _enemys[i].Died -= OnEnemyDied;
        }

        private IEnumerator AimProcess()
        {
            while (_currentTarget != null)
            {
                if (_enemys.Count <= 0)
                {
                    _currentTarget = null;
                    yield break;
                }

                FindClosestEnemy();
                Vector2 direction = _weaponPoint.transform.GetVector2Direction(_currentTarget.transform);
                Rotation(direction);

                yield return null;
            }
        }

        private void FindClosestEnemy()
        {
            float squaredClosestDistance = Mathf.Infinity;

            foreach (var enemy in _enemys)
            {
                Vector3 directionToTarget = _weaponPoint.transform.position - enemy.transform.position;
                float squaredDirection = directionToTarget.sqrMagnitude;

                if (squaredDirection < squaredClosestDistance)
                {
                    _currentTarget = enemy;
                    squaredClosestDistance = squaredDirection;
                }
            }
        }

        private void Rotation(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _weaponPoint.transform.eulerAngles = new Vector3(0, 0, angle);
        }

        private void OnTriggerEnterObserver(Collider2D collider)
        {
            if (!collider.TryGetComponent<Enemy>(out var enemy))
                return;

            _enemys.Add(enemy);
            enemy.Died += OnEnemyDied;
            _currentTarget = enemy;

            _aimProcess = StartCoroutine(AimProcess());
        }

        private void OnTriggerExitObserver(Collider2D collider)
        {
            if (!collider.TryGetComponent<Enemy>(out var enemy))
                return;

            _enemys.Remove(enemy);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            _enemys.Remove(enemy);
        }
    }
}
