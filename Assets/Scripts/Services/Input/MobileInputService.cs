using System;
using UI;
using UnityEngine;
using Zenject;

namespace Services.Input
{
    public class MobileInputService : IInputService, IDisposable
    {
        private readonly UIInputModel _inputModel;

        public Vector2 Axis => GetAxisByJoystick();

        private Joystick Joystick => _inputModel.Joystick;

        public event Action PressedShoot;
        public event Action PressedOpenInventory;

        public MobileInputService(UIInputModel uIInputModul)
        {
            if (uIInputModul is null)
                throw new ArgumentNullException(nameof(uIInputModul));

            _inputModel = uIInputModul;

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

        public void Dispose()
        {
            _inputModel.ShootButton.HoldButton -= OnShootHolding;
            _inputModel.InventaryButton.HoldButton -= OnInventoryOpen;
        }
    }
}
