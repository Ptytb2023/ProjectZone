using System;
using UnityEngine;

namespace Shooting.Settings
{
    [Serializable]
    public class GunProjectileSettings
    {
        [field: SerializeField] public Transform ShootPoint { get; private set; }
    }
}
