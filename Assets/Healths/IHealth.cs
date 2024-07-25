using ReactivePropertes;
using System;

namespace Enemys
{
    public interface IHealth
    {
        IReadOnlyReactiveProperty<float> Value { get; }

        event Action Died;

        void TakeDamage(float damage);
    }
}