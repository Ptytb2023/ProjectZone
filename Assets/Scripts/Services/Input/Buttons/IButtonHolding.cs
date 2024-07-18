using System;

namespace Services.Input.Buttons
{
    public interface IButtonHolding
    {
        event Action DownButton;
        event Action HoldButton;
        event Action UpButton;
    }
}