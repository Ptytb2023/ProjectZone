using Shooting.Weapons;
using System;
using UnityEngine;

namespace Shooting
{
    public class ShootingRate
    {
        private int _shotsPerSecond;
        private float _lastShotTimestamp;
        private float _intervalBetweenShots;

        public int ShotsPerSecond => _shotsPerSecond;

        public ShootingRate(int shotsPerSecond) =>
            SetShotsPerSecond(shotsPerSecond);

        public void SetShotsPerSecond(int shotsPerSecond)
        {
            if (shotsPerSecond <= 0)
                throw new ArgumentException("The number of shots must be greater than zero.");

            _shotsPerSecond = shotsPerSecond;
            _intervalBetweenShots = 1.0f / _shotsPerSecond;
        }

        public void AttemptToShoot(IWeapon weapon)
        {
            if (!CanShoot())
                return;

            weapon.Shoot();
            _lastShotTimestamp = Time.time;
        }

        public void AttemptToShoot(IGun gun)
        {
            if (gun.IsReloading)
                return;

            AttemptToShoot(gun);
        }

        private bool CanShoot() =>
            Time.time - _lastShotTimestamp >= _intervalBetweenShots;
    }
}
