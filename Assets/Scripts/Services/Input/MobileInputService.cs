using Services.Input.Buttons;
using System;
using UI;
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

        public MobileInputService(UIInputModel uIInputModul)
        {
            if(uIInputModul is null)
                throw new ArgumentNullException(nameof(uIInputModul));

            _joystick = uIInputModul.Joystick;
            _buttonShoot = uIInputModul.ShootButton;
            _buttonInventory = uIInputModul.InventaryButton;

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
