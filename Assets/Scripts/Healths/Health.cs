using ReactivePropertes;
using System;
using UnityEngine;

namespace Enemys
{
    public interface IDamageble
    {
        event Action Died;
        void TakeDemage(float damege);
    }

    public class Health : MonoBehaviour
    {
        [SerializeField] private float _startHelth;

        private IReactiveProperty<float> _health = new ReactiveProperty<float>();

        public IReadOnlyReactiveProperty<float> Value => _health;

        private void Awake() => 
            ResetHealth();

        public void TakeDamage(float damage)
        {
            ValidateDamage(damage);

            float newHealth = _health.Value - damage;

            if (newHealth <= 0)
                _health.Value = 0;
            else
                _health.Value = newHealth;
        }

        private void ValidateDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage cannot be negative", nameof(damage));
        }

        public void ResetHealth() => 
            _health.Value = _startHelth;
    }
}
