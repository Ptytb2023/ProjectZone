﻿using System;
using Factorys;
using Services.Input;
using Shooting.Weapons;
using UnityEngine;
using Zenject;

namespace Shooting
{
    public class WeaponSystem : MonoBehaviour, IWeaponSystem
    {
        [SerializeField] private Transform _weaponPoint;

        private IFactoryObject _factory;
        private BaseWeapon _currentWeapon;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService, IFactoryObject factory) 
        {
            _inputService = inputService;
            _factory = factory;
        }

        private void OnEnable() =>
            _inputService.PressedShoot += OnPressedShoot;

        private void OnDisable() =>
            _inputService.PressedShoot -= OnPressedShoot;

        public void EquipWeapon(BaseWeapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon), "The weapon cannot be null.");

            DeactivateCurrentWeapon();

            _currentWeapon = _factory.Creat(weapon);
        }

        private void DeactivateCurrentWeapon()
        {
            if (_currentWeapon is not null)
                Destroy(_currentWeapon.gameObject);
        }

        private void OnPressedShoot() =>
            _currentWeapon.TryShoot();

    }
}
