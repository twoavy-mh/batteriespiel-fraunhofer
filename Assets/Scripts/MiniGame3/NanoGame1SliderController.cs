using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Minigame3
{
    
    public class NanoGame1SliderController : MonoBehaviour, IPointerUpHandler
    {
        public event Action OnValueChange;

        public int correctSliderValue = 10;
        public GameObject icon;
        public ImageColorSetter fillColor;

        private Slider _sliderComponent;
        private bool _isCorrect = false;
        private bool _isSolved = false;

        private int sliderOffsetValue = 3;

        // Start is called before the first frame update
        void Start()
        {
            _sliderComponent = GetComponent<Slider>();
            _sliderComponent.onValueChanged.AddListener(ValueChange);
        }

        // Update is called once per frame
        void Update()
        {
            if (_sliderComponent.value <= correctSliderValue + sliderOffsetValue &&
                _sliderComponent.value >= correctSliderValue - sliderOffsetValue)
            {
                _isCorrect = true;
                icon.SetActive(true);
            }
            else
            {
                _isCorrect = false;
                icon.SetActive(false);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isCorrect && _sliderComponent.interactable)
            {
                SetSolved();
            }
        }

        public void SetSolved()
        {
            _isSolved = true;
            _sliderComponent.interactable = false;
            fillColor.UpdateColor(Tailwind.Yellow3, 1f);
        }

        public float GetCurrentValue()
        {
            return _sliderComponent.value;
        }

        public bool GetIsCorrect()
        {
            return _isCorrect;
        }

        public bool GetIsSolved()
        {
            return _isSolved;
        }
        
        public void SetCurrentValue(float value)
        {
            _sliderComponent.value = value;
        }
        
        private void ValueChange(float value)
        {
            OnValueChange?.Invoke();
        }
    }
}
