using Services.Input;
using Shooting.Weapons;
using UnityEngine;
using Zenject;
using System;

namespace Shooting
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField] private Transform _gunParent;

        private IGun _currentWeapon;
        private IInputService _inputService;

        private ShootingRate _shootingRate = new ShootingRate();

        [Inject]    
        private void Construct(IInputService inputService) =>
            _inputService = inputService;
    
        private void OnEnable() =>
            _inputService.PressedShoot += OnPressedShoot;

        private void OnDisable() =>
            _inputService.PressedShoot -= OnPressedShoot;


        public void EquipWeapon(IGun gun)
        {
            if (gun is null)
                throw new NullReferenceException($"the passed parameter {nameof(gun)} is missing null");

            DeactivateCurrentWeapon();

            _currentWeapon = gun;
            _currentWeapon.AmmoChanged.Subscribe(OnAmmoChanged);
            _shootingRate.SetShotsPerSecond(_currentWeapon.Settings.ShotsPerSecond);
        }

        private void DeactivateCurrentWeapon()
        {
            if (_currentWeapon is null)
                return;

            _currentWeapon.SetActive(false);
            _currentWeapon.AmmoChanged.UnSubscribe(OnAmmoChanged);
        }

        private void OnAmmoChanged(int count)
        {
            if (count > 0)
                return;

            _currentWeapon.Reload();
        }

        private void OnPressedShoot() =>
            _shootingRate.AttemptToShoot(_currentWeapon);
    }
}
