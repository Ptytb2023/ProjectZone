using UnityEngine;
using Shooting.Settings;
using System.Collections;
using Shooting.Projectiles;
using PoolObject;
using Zenject;
using Extensions;
using Inventarys;
using Inventorys.Structures;
using System;

namespace Shooting.Weapons
{

    public class GunProjectile : BaseGun
    {
        private WaitForSeconds _weaponRate;
        private WaitForSeconds _weaponReloadTime;

        private IInventoryController _inventoryController;

        private IPool<Projectile> _pool;

        private Transform ShootPoint => GunSettings.ShootPoint;


        [Inject]
        private void Construct(IPool<Projectile> pool, IInventoryController inventoryController)
        {
            _inventoryController = inventoryController;
            _pool = pool;
        }

        private void Start()
        {
            float second = AmmoReloadSettings.ReloadTime;
            _weaponReloadTime = new WaitForSeconds(second);

            float secondDelay = 1.0f / Settings.ShotsPerSecond;
            _weaponRate = new WaitForSeconds(secondDelay);

        }

        protected override IEnumerator PerformReload(Action onComplited)
        {
            yield return _weaponReloadTime;

            int count = AmmoReloadSettings.MagazineSize;
            string idItem = AmmoReloadSettings.ItemIdBulelt;

            RemoveItemResult result = _inventoryController.RemoveAvailableItems(idItem, count);

            if (result.Success)
                CurrentAmmo.Value = result.AmountRemoved;

            onComplited?.Invoke();
        }

        protected override IEnumerator PerformShoot(Action onComplited)
        {
            var bullet = _pool.Request();

            Vector2 position = ShootPoint.position;
            Vector2 direction = ShootPoint.GetDirection();

            float damage = Settings.BaseDamage;

            bullet.Shoot(position, direction, damage);

            yield return _weaponRate;
            onComplited?.Invoke();
        }
    }
}
