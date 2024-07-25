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

        private bool _isCanShoot;

        public IReadOnlyReactiveProperty<int> Ammo => CurrentAmmo;

        private void Awake() => 
            CurrentAmmo = new ReactiveProperty<int>(AmmoReloadSettings.MagazineSize);

        public override void TryShoot()
        {
            if (CurrentAction is not null)
                return;

            if (CurrentAmmo.Value <= 0)
            {
                CurrentAction = StartCoroutine(PerformReload(ResetAction));
                return;
            }

            CurrentAmmo.Value--;
            CurrentAction = StartCoroutine(PerformShoot(ResetAction));
        }

        public void Reload()
        {
            if (CurrentAction is not null)
                return;

            CurrentAction = StartCoroutine(PerformReload(ResetAction));
        }

        private void ResetAction() =>
            CurrentAction = null;

        protected abstract IEnumerator PerformReload(Action onComplete);
        protected abstract IEnumerator PerformShoot(Action onComplete);
    }
}
