using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.Input.Buttons
{
    public class ButtonHolding : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IButtonHolding
    {
        private Coroutine _holdingRoutine;

        public event Action DownButton;
        public event Action UpButton;
        public event Action HoldButton;

        public void OnPointerDown(PointerEventData eventData)
        {
            DownButton?.Invoke();

            _holdingRoutine = StartCoroutine(ProcessHoldingInput());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UpButton?.Invoke();

            StopCoroutine(_holdingRoutine);
        }

        private IEnumerator ProcessHoldingInput()
        {
            while (enabled)
            {
                HoldButton?.Invoke();
                yield return null;
            }
        }
    }
}
