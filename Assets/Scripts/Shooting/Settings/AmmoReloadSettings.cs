using System;
using UnityEngine;

namespace Shooting.Settings
{
    [Serializable]
    public class AmmoReloadSettings
    {
        [field: SerializeField] public string ItemIdBulelt { get; private set; }
        [field: SerializeField] public int MagazineSize { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }

        public AmmoReloadSettings(int maximumAmmo, float reloadTime)
        {
            MagazineSize = maximumAmmo;
            ReloadTime = reloadTime;
        }
    }
}
