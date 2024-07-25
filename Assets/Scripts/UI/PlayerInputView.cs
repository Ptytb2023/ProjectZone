using Services.Input.Buttons;
using UnityEngine;

namespace UI
{
    public class PlayerInputView : MonoBehaviour
    {
        [field: SerializeField] public Joystick Joystick { get; private set; }
        [field: SerializeField] public ButtonHolding ShootButton { get; private set; }
        [field: SerializeField] public ButtonHolding InventaryButton { get; private set; }

       
    }
}
