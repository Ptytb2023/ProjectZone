using System;
using UnityEngine;

namespace DataPersistence
{
    [Serializable]
    public class WeaponData
    {
        [field: SerializeField] public int BulletsAtOneShoot { get; private set; }
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public int DelayBeetweenShoot { get; private set; }
        [field: SerializeField] public float TimeReload { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
