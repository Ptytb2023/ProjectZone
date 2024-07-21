using System;
using UI;
using UnityEngine;
using Zenject;

namespace Services.Input
{
    public class MobileInputService : IInputService, IDisposable
    {
        private readonly PlayerInputMobile _inputModel;

        public Vector2 Axis => GetAxisByJoystick();

        private Joystick Joystick => _inputModel.Joystick;

        public event Action PressedShoot;
        public event Action PressedOpenInventory;

        public MobileInputService(PlayerInputMobile playerInput)
        {
            if (playerInput is null)
                throw new ArgumentNullException(nameof(playerInput));

            _inputModel = playerInput;

            SubscribeToEvents();
        }

        public void SetActive(bool active) => 
            _inputModel.gameObject.SetActive(active);

        private Vector2 GetAxisByJoystick() =>
           new Vector2(Joystick.Horizontal, Joystick.Vertical);

        private void SubscribeToEvents()
        {
            _inputModel.ShootButton.HoldButton += OnShootHolding;
            _inputModel.InventaryButton.HoldButton += OnInventoryOpen;
        }

        private void OnShootHolding() =>
           PressedShoot?.Invoke();

        private void OnInventoryOpen() =>
            PressedOpenInventory?.Invoke();

        private void UnsbcribeEvents()
        {
            _inputModel.ShootButton.HoldButton -= OnShootHolding;
            _inputModel.InventaryButton.HoldButton -= OnInventoryOpen;
        }

        public void Dispose() =>
           UnsbcribeEvents();
    }
}
