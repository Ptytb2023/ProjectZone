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

        private ShootingRate _shootingRate;

        [Inject]
        private void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void OnEnable() => 
            _inputService.PressedShoot += OnPressedShoot;

        private void OnDisable() => 
            _inputService.PressedShoot -= OnPressedShoot;


        public void SetGun(IGun gun)
        {
            if (gun is null)
                throw new NullReferenceException($"the passed parameter {nameof(gun)} is missing null");

            _currentWeapon.SetActive(false);
            _currentWeapon?.AmmoChanged.UnSubscribe(OnAmmoChanged);

            _currentWeapon = gun;
            _currentWeapon.AmmoChanged.Subscribe(OnAmmoChanged);
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
