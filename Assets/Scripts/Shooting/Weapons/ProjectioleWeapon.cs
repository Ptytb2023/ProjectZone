using Pool;
using Shooting.Projectile;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Shooting.Weapons
{
    public class ProjectioleWeapon : BaseWeapon
    {
        private WaitForSeconds _reloadTime;
        private WaitForSeconds _delayBetweenShoot;

        private IPool<IProjectile> _pool;

        private void Start()
        {
            float secondReload = WeaponData.TimeReload;
            float secondDelay = WeaponData.DelayBeetweenShoot;

            _reloadTime = new WaitForSeconds(secondReload);
            _delayBetweenShoot = new WaitForSeconds(secondDelay);
        }

        [Inject]
        private void Construct(IPool<IProjectile> pool) => 
            _pool = pool;

        public override IEnumerator Reload()
        {
            yield return _reloadTime;

            CurrentAmmo.Value = WeaponData.MaxAmmo;
        }

        public override IEnumerator Shoot()
        {
            Vector3 position = PointShoot.position;
            Vector3 direction = PointShoot.forward;
            int damage = WeaponData.Damage;

            var bullet = _pool.Request();
            bullet.Shoot(position, direction, damage);

            yield return _delayBetweenShoot;
        }
    }
}
