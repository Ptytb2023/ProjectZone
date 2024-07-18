using Services.Input.Buttons;
using UnityEngine;

namespace UI
{
    public class UIInputModel : MonoBehaviour
    {
        [field: SerializeField] public Joystick Joystick { get; private set; }
        [field: SerializeField] public ButtonHolding ShootButton { get; private set; }
        [field: SerializeField] public ButtonHolding InventaryButton { get; private set; }

        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}
