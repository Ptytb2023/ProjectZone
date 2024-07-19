﻿using Shooting.Projectiles;
using System;
using UnityEngine;

namespace Shooting.Settings
{
    [Serializable]
    public class GunProjectileSettings
    {
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    }
}