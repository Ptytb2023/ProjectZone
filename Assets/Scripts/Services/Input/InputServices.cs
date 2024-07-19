using System;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }

        public event Action PressedShoot;
        public event Action PressedOpenInventory;

        public void SetActive(bool active);
    }
}