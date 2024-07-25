using ReactivePropertes;
using System;
using UnityEngine;

namespace Healths
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maximumHealth;

        private IReactiveProperty<float> _currentHealth = new ReactiveProperty<float>();

        public IReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;
        public float MaximumHealth => _maximumHealth;

        private void Awake() =>
            InitializeHealth();

        public void InflictDamage(float damageAmount)
        {
            ValidateDamageAmount(damageAmount);

            float updatedHealth = _currentHealth.Value - damageAmount;

            if (updatedHealth <= 0)
                _currentHealth.Value = 0;
            else
                _currentHealth.Value = updatedHealth;
        }

        private void ValidateDamageAmount(float damageAmount)
        {
            if (damageAmount < 0)
                throw new ArgumentException("Damage cannot be negative", nameof(damageAmount));
        }

        public void InitializeHealth() =>
            _currentHealth.Value = _maximumHealth;
    }
}
