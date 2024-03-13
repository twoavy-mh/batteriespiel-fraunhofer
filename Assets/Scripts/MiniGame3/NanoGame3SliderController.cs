using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Minigame3
{
    
    public class NanoGame3SliderController : MonoBehaviour, IPointerUpHandler
    {
        public event Action OnValueChange;
        
        public enum IsInRange
        {
            toLow,
            inRange,
            toHigh,
        }
        
        public float correctSliderValue = .5f;

        private Slider _sliderComponent;
        private bool _isCorrect = false;
        private bool _isSolved = false;

        private float sliderOffsetValue = 0.05f;

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
            }
            else
            {
                _isCorrect = false;
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
        }

        public float GetCurrentValue()
        {
            return _sliderComponent.value;
        }

        public bool GetIsSolved()
        {
            return _isSolved;
        }
        
        public IsInRange IsValueInRange()
        {
            if (_sliderComponent.value > correctSliderValue + sliderOffsetValue)
            {
                return IsInRange.toHigh;
            } 
            
            if (_sliderComponent.value < correctSliderValue - sliderOffsetValue)
            {
                return IsInRange.toLow;
            }
            
            return IsInRange.inRange;
        }
        
        private void ValueChange(float value)
        {
            OnValueChange?.Invoke();
        }
    }
}
