using ReactivePropertes;
using UnityEngine;
using Shooting.Settings;
using System.Collections;
using Lean.Pool;
using Shooting.Projectiles;

namespace Shooting.Weapons
{
    public class GunProjectile : MonoBehaviour, IGun
    {
        [SerializeField] private GunProjectileSettings _gunSettings;
        [SerializeField] private WeaponSettings _weaponSettings;
        [SerializeField] private AmmoReloadSettings _ammoReloadSettings;

        private WaitForSeconds _weaponReloadTime;
        private IReactiveProperty<int> _ammoCount = new ReactiveProperty<int>();

        private Transform ShootPoint => _gunSettings.ShootPoint;

        public bool IsReloading { get; private set; }
        public ReactivePropertes.IObservable<int> AmmoChanged => _ammoCount;
        public WeaponSettings Settings => _weaponSettings;


        private void Start()
        {
            var second = _ammoReloadSettings.ReloadTime;
            _weaponReloadTime = new WaitForSeconds(second);
        }

        private void OnEnable() =>
            IsReloading = false;

        public void Reload() =>
            StartCoroutine(ReloadCoroutine());

        public void Shoot()
        {
            _ammoCount.Value--;

            var bullet = GetBullet();

            Vector2 position = ShootPoint.position;
            Vector2 direction = GetDirection();
            float damage = _weaponSettings.BaseDamage;

            bullet.Shoot(position, direction, damage);
        }

        private Projectile GetBullet()
        {
            var prefab = _gunSettings.ProjectilePrefab;
            return LeanPool.Spawn(prefab);
        }

        private Vector2 GetDirection()
        {
            float angle = ShootPoint.eulerAngles.z;
            Vector2 direction =
                new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad));

            return direction;
        }

        private IEnumerator ReloadCoroutine()
        {
            IsReloading = true;
            yield return _weaponReloadTime;

            _ammoCount.Value = _ammoReloadSettings.MaximumAmmo;
            IsReloading = false;
        }

        public void SetActive(bool value) =>
            gameObject.SetActive(value);

    }
}
