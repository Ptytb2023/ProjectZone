using Services.Input;
using Shooting.Weapons;
using UnityEngine;
using Zenject;
using System;

namespace Shooting
{
    public class WeaponSystem : MonoBehaviour, IWeaponSystem
    {
        [SerializeField] private Transform _gunParent;

        private IWeapon _currentWeapon;
        private IInputService _inputService;

        private ShootingRate _shootingRate = new ShootingRate();
        private Action Reloading;

        [Inject]
        private void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void OnEnable() =>
            _inputService.PressedShoot += OnPressedShoot;

        private void OnDisable() =>
            _inputService.PressedShoot -= OnPressedShoot;


        public void EquipWeapon(IWeapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon), "The weapon cannot be null.");

            DeactivateCurrentWeapon();
            _currentWeapon = weapon;

            if (_currentWeapon is IGun gun)
            {
                gun.AmmoChanged.Subscribe(HandleAmmoChanged);
                Reloading += gun.Reload;
            }

            _shootingRate.SetShotsPerSecond(_currentWeapon.Settings.ShotsPerSecond);
        }

        private void DeactivateCurrentWeapon()
        {
            if (_currentWeapon == null)
                return;

            _currentWeapon.SetActive(false);

            if (_currentWeapon is IGun gun)
            {
                gun.AmmoChanged.Unsubscribe(HandleAmmoChanged);
                Reloading -= gun.Reload;
            }
        }

        private void HandleAmmoChanged(int count)
        {
            if (count > 0)
                return;

            Reloading?.Invoke();
        }

        private void OnPressedShoot() =>
            _shootingRate.AttemptToShoot(_currentWeapon);
    }
}
