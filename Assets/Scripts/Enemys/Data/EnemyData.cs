using System;
using UnityEngine;

namespace Enemys
{
    [Serializable]
    public class EnemyData
    {
        [field: SerializeField] public float RadiusAgro { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DelayBetweenAttack { get; private set; }
        [field: SerializeField] public float DistanceAttack { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}
