using UnityEngine;

namespace Enemys
{
    public class RangeEvaluator
    {
        private float _distanceAttack;
        private Transform _transform;

        public RangeEvaluator(Transform transform) =>
            _transform = transform;

        public RangeEvaluator(float distanceAttack, Transform transform)
        {
            _distanceAttack = distanceAttack;
            _transform = transform;
        }

        public bool AttackZoneValidator(Vector3 target)
        {
            Vector3 direction = target - _transform.position;

            return direction.sqrMagnitude < _distanceAttack * 2;
        }
    }
}
