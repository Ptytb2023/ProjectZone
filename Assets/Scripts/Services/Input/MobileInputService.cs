using Services.Input.Buttons;
using System;
using UnityEngine;

namespace Services.Input
{
    public class MobileInputService : IInputService, IDisposable
    {
        private readonly Joystick _joystick;
        private readonly ButtonHolding _buttonShoot;
        private readonly ButtonHolding _buttonInventory;

        public Vector2 Axis => GetAxisByJoystick();

        public event Action PressedShoot;
        public event Action PressedOpenInventory;

        public MobileInputService(Joystick joystick, ButtonHolding buttonShoot, ButtonHolding buttonInventory)
        {
            _joystick = joystick ?? throw new ArgumentNullException(nameof(joystick));
            _buttonShoot = buttonShoot ?? throw new ArgumentNullException(nameof(buttonShoot));
            _buttonInventory = buttonInventory ?? throw new ArgumentNullException(nameof(buttonInventory));

            SubscribeToEvents();
        }


        private Vector2 GetAxisByJoystick() =>
           new Vector2(_joystick.Horizontal, _joystick.Vertical);

        private void OnShootHolding() =>
            PressedShoot?.Invoke();

        private void OnInventoryOpen() =>
            PressedOpenInventory?.Invoke();


        private void SubscribeToEvents()
        {
            _buttonShoot.DownButton += OnShootHolding;
            _buttonInventory.HoldButton += OnInventoryOpen;
        }

        public void Dispose()
        {
            _buttonShoot.DownButton -= OnShootHolding;
            _buttonInventory.HoldButton -= OnInventoryOpen;
        }
    }
}
