using System;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        public Vector2 Axis { get; }

        public event Action PressedShoot;
        public event Action PressedOpenInventory;
    }
}