using ReactivePropertes;
using UnityEngine;
using Shooting.Settings;
using System.Collections;
using System;

namespace Shooting.Weapons
{
    public abstract class BaseGun : BaseWeapon, IGun
    {
        [SerializeField] protected GunProjectileSettings GunSettings;
        [SerializeField] protected AmmoReloadSettings AmmoReloadSettings;

        protected Coroutine CurrentAction;
        protected IReactiveProperty<int> CurrentAmmo;

        protected bool IsCanShoot;

        public event Action<float> ReloadStart;

        public IReadOnlyReactiveProperty<int> Ammo => CurrentAmmo;

        private void Awake()
        {
            IsCanShoot = true;
            CurrentAmmo = new ReactiveProperty<int>(AmmoReloadSettings.MagazineSize);
        }

        public override void TryShoot()
        {
            if (!IsCanShoot)
                return;

            if (CurrentAmmo.Value <= 0)
            {
                Reload();
                return;
            }

            CurrentAmmo.Value--;
            CurrentAction = StartCoroutine(PerformShoot());
            IsCanShoot = false;
        }

        public void Reload()
        {
            if (!IsCanShoot)
                return;

            ReloadStart?.Invoke(AmmoReloadSettings.ReloadTime);
            CurrentAction = StartCoroutine(PerformReload());
            IsCanShoot = false;
        }


        protected abstract IEnumerator PerformReload();
        protected abstract IEnumerator PerformShoot();
    }
}
