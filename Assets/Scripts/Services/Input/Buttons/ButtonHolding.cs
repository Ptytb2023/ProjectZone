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

            if (_holdingRoutine == null)
                _holdingRoutine = StartCoroutine(ProcessHoldingInput());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UpButton?.Invoke();

            if (_holdingRoutine is not null)
            {
                StopCoroutine(_holdingRoutine);
                _holdingRoutine = null;
            }
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
