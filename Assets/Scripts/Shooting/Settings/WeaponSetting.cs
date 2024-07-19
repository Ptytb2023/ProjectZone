using System;
using UnityEngine;

namespace Shooting.Settings
{
    [Serializable]
    public class WeaponSettings
    {
        [field: SerializeField] public int ShotsPerSecond { get; private set; }
        [field: SerializeField] public int BaseDamage { get; private set; }

        public WeaponSettings(int shotsPerSecond, int baseDamage)
        {
            ShotsPerSecond = shotsPerSecond;
            BaseDamage = baseDamage;
        }
    }
}
