using Enemys;
using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Health))]
    public class Hero : MonoBehaviour, IDamageble
    {
        private Health _health;


        public event Action Died;

        private void Awake() =>
          _health = GetComponent<Health>();

        private void Start() =>
           _health.Value.Subscribe(OnChangeHealth);

        private void OnDestroy() =>
            _health.Value.Unsubscribe(OnChangeHealth);

        public void TakeDemage(float damege) =>
            _health.TakeDamage(damege);

        private void OnChangeHealth(float value)
        {
            if (value <= 0)
            {
                Died?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
