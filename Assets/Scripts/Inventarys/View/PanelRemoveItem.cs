using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Inventarys.View
{
    public class PanelRemoveItem : MonoBehaviour
    {
        private const int MinItemRemove = 1;

        [SerializeField] private TMP_Text _textForItemToRemove;
        [SerializeField] private Slider _sliderForToRemoveItems;
        [SerializeField] private Button _buttonRemove;

        public event Action<int> ClickRemove;

        private void Start() =>
            _sliderForToRemoveItems.minValue = MinItemRemove;

        private void OnEnable()
        {
            _sliderForToRemoveItems.onValueChanged.AddListener(OnSliderValueChange);
            _buttonRemove.onClick.AddListener(OnClickRemove);
        }

        private void OnDisable()
        {
            _sliderForToRemoveItems.onValueChanged.RemoveListener(OnSliderValueChange);
            _buttonRemove.onClick.RemoveListener(OnClickRemove);
        }

        private void OnClickRemove()
        {
            int amountRemove = (int)_sliderForToRemoveItems.value;
            ClickRemove?.Invoke(amountRemove);

            gameObject.SetActive(false);
        }

        private void OnSliderValueChange(float value) =>
            _textForItemToRemove.text = value.ToString();

        public void SetMaxRemoveAndOpenPanel(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException($"{value} not be less than or equal to 0");
            UpdateValue(value);

            gameObject.SetActive(true);
        }

        private void UpdateValue(int value)
        {
            _sliderForToRemoveItems.maxValue = value;
            OnSliderValueChange(value / 2);
            _sliderForToRemoveItems.value = value / 2;
        }

        private void OnValidate()
        {
            if (_sliderForToRemoveItems is not null)
                _sliderForToRemoveItems.wholeNumbers = true;
        }
    }
}
