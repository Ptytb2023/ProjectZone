using System;
using UnityEngine;

namespace Shooting.Settings
{
    [Serializable]
    public class AmmoReloadSettings
    {
        [field: SerializeField] public int MaximumAmmo { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }

        public AmmoReloadSettings(int maximumAmmo, float reloadTime)
        {
            MaximumAmmo = maximumAmmo;
            ReloadTime = reloadTime;
        }
    }
}
