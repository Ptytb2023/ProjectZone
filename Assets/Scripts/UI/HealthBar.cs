using Healths;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private Health _health;

        private void OnEnable() => 
            _health.CurrentHealth.SubscribeAndUpdate(OnChageHealth);

        private void OnDisable() =>
            _health.CurrentHealth.Unsubscribe(OnChageHealth);

        private void OnChageHealth(float value) => 
            _healthBarSlider.value = value / _health.MaximumHealth;
    }
}
